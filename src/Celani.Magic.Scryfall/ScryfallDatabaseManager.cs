using Celani.Magic.Downloader.Storage;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public class ScryfallDatabaseManager(IScryfallApi scryfallApi, IDbContextFactory<MagicContext> scryfallContextFactory, IHttpClientFactory httpClientFactory)
{
    private IScryfallApi ScryfallApi { get; } = scryfallApi;

    private IDbContextFactory<MagicContext> ScryfallContextFactory { get; } = scryfallContextFactory;

    private IHttpClientFactory ClientFactory { get; } = httpClientFactory;

    private static readonly JsonSerializerOptions BulkDataDownloadOptions = new()
    {
        DefaultBufferSize = 128,
        AllowTrailingCommas = true,
        Converters = { new JsonStringEnumConverter() }
    };

    private record CardDelta
    {
        public required ScryfallMetadata MetadataRecord { get; init; }

        public required Dictionary<string, ScryfallCard> ScryfallCards { get; init; }

        public required Dictionary<string, OracleCard> OracleCards { get; init; }

        public List<ScryfallCard> ScryfallAdded { get; } = [];

        public List<OracleCard> OracleAdded { get; } = [];

        public OracleCard GetOracleCard(ScryfallCardObject card)
        {
            if (!OracleCards.TryGetValue(card.CompositeOracleId, out var oracleCard))
            {
                oracleCard = new OracleCard
                {
                    Name = card.Name,
                    OracleId = card.CompositeOracleId,
                    ColorIdentity = ColorIdentityTools.FromColors(card.ColorIdentity ?? []),
                    CardTypes = CardTypeTools.FromTypeLine(card.CompositeTypeLine),
                    RepresentativeScryfallId = card.Id,
                    OracleText = card.CompositeOracleText ?? "",
                    ManaCost = card.CompositeManaCost ?? "",
                    TypeLine = card.CompositeTypeLine
                };
            }
            else
            {
                oracleCard.Name = card.Name;
                oracleCard.ColorIdentity = ColorIdentityTools.FromColors(card.ColorIdentity ?? []);
                oracleCard.CardTypes = CardTypeTools.FromTypeLine(card.CompositeTypeLine);
                oracleCard.RepresentativeScryfallId = card.Id;
                oracleCard.OracleText = card.CompositeOracleText ?? "";
                oracleCard.ManaCost = card.CompositeManaCost ?? "";
                oracleCard.TypeLine = card.CompositeTypeLine;
            }

            OracleCards[card.CompositeOracleId] = oracleCard;
            OracleAdded.Add(oracleCard);
            return oracleCard;
        }

        public ScryfallCard GetScryfallCard(ScryfallCardObject card, OracleCard oracleCard)
        {
            if (!ScryfallCards.TryGetValue(card.Id, out var scryfallCard))
            {
                scryfallCard = new ScryfallCard
                {
                    ScryfallId = card.Id,
                    OracleCard = oracleCard,
                };

                ScryfallCards[card.Id] = scryfallCard;
                ScryfallAdded.Add(scryfallCard);
            }

            return scryfallCard;
        }

        public void MergeScryfallIds(string oldId, string newId)
        {
            // If the old ID is already in the database, we don't need to do anything.
            if (ScryfallCards.ContainsKey(oldId)) return;

            // If the new ID is not in the database, we can't do anything.
            if (!ScryfallCards.TryGetValue(newId, out var newCard)) return;

            // Map the old Scryfall ID to the new Scryfall ID's Oracle card:
            var scryfallCard = new ScryfallCard
            {
                ScryfallId = oldId,
                OracleCard = newCard.OracleCard
            };

            ScryfallCards[oldId] = scryfallCard;
            ScryfallAdded.Add(scryfallCard);
        }

        public void MarkDeletedScryfallId(string deletedId, string oracleId)
        {
            if (ScryfallCards.ContainsKey(deletedId)) return;
            if (!OracleCards.TryGetValue(oracleId, out var oracleCard)) return;
            
            var scryfallCard = new ScryfallCard
            {
                ScryfallId = deletedId,
                OracleCard = oracleCard
            };

            ScryfallCards[deletedId] = scryfallCard;
            ScryfallAdded.Add(scryfallCard);
        }
    }
    public async Task UpdateDatabaseAsync(bool force)
    {
        using var scryfallContext = ScryfallContextFactory.CreateDbContext();

        // Get the bulk data:
        var bulkData = await ScryfallApi.GetBulkDataAsync();

        // Get all the card objects on Scryfall:
        var allCards = bulkData.Data.First(x => x.DataType == BulkDataType.AllCards);

        // Get the metadata about the last time this database was updated.
        var metadataRecord = scryfallContext.Metadata.OrderByDescending(x => x.Id).FirstOrDefault();
        var lastUpdated = metadataRecord?.CreatedAt;

        // If the bulk update happened before the last updated time, no new information will be added.
        if (lastUpdated is not null && allCards.UpdatedAt <= lastUpdated && !force) return;

        var delta = new CardDelta
        {
            OracleCards = await scryfallContext.OracleCards.ToDictionaryAsync(x => x.OracleId),
            ScryfallCards = await scryfallContext.ScryfallCards.Include(x => x.OracleCard).ToDictionaryAsync(x => x.ScryfallId),
            MetadataRecord = metadataRecord ?? new()
        };

        delta.MetadataRecord.CreatedAt = allCards.UpdatedAt;
        delta.MetadataRecord.DownloadUri = allCards.DownloadUri!;

        // Download and stream the bulk data:
        using var client = ClientFactory.CreateClient("BulkData");

        var oracleCards = bulkData.Data.First(x => x.DataType == BulkDataType.OracleCards);
        using var oracleStream = await client.GetStreamAsync(oracleCards.DownloadUri);
        var oracleCardEnum = JsonSerializer.DeserializeAsyncEnumerable<ScryfallCardObject>(oracleStream, BulkDataDownloadOptions);

        await foreach (var card in oracleCardEnum)
        {
            if (card is null) continue;
            if (card.CompositeOracleId is null) continue;
            var oracleCard = delta.GetOracleCard(card);
        }

        using var stream = await client.GetStreamAsync(allCards.DownloadUri);
        var cardEnum = JsonSerializer.DeserializeAsyncEnumerable<ScryfallCardObject>(stream, BulkDataDownloadOptions);

        await foreach (var card in cardEnum)
        {
            if (card is null) continue;
            if (card.CompositeOracleId is null) continue;
            var oracleCard = delta.OracleCards[card.CompositeOracleId];
            var scryfallCard = delta.GetScryfallCard(card, oracleCard);
        }

        // Now for the migrations:
        var migrations = ScryfallApi.GetAllMigrationsAsync();

        await foreach (var migration in migrations)
        {
            if (migration.MigrationStrategy == MigrationStrategy.Merge)
            {
                delta.MergeScryfallIds(migration.OldScryfallId, migration.NewScryfallId!);
            }
            else if (migration.MigrationStrategy == MigrationStrategy.Delete)
            {
                var oracleId = migration.Metadata?.OracleId;
                if (oracleId is null) continue;

                delta.MarkDeletedScryfallId(migration.OldScryfallId, oracleId);
            }
        }

        scryfallContext.UpdateRange(delta.OracleAdded);
        scryfallContext.UpdateRange(delta.ScryfallAdded);
        scryfallContext.Update(delta.MetadataRecord);
        await scryfallContext.SaveChangesAsync();
    }
}
