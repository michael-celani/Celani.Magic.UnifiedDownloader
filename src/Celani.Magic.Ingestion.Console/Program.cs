using Celani.Magic.Downloader.Core;
using Celani.Magic.Downloader.Moxfield;
using Celani.Magic.Downloader.Storage;
using Celani.Magic.Ingestion.Console;
using Celani.Magic.ML;
using Celani.Magic.Scryfall;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddLogging();
builder.Services.AddScryfall(builder.Configuration);
builder.Services.AddMagicStorage(builder.Configuration);
builder.Services.AddMoxfield(builder.Configuration);
builder.Services.AddMagicML(builder.Configuration);

IHost host = builder.Build();

var dbContextFactory = host.Services.GetRequiredService<IDbContextFactory<MagicContext>>();
using var dbContext = dbContextFactory.CreateDbContext();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
var scryfallApi = host.Services.GetRequiredService<IScryfallApi>();
var moxfieldApi = host.Services.GetRequiredService<IMoxfieldApi>();
var moxfieldDownloader = host.Services.GetRequiredService<IMagicDownloader>();

// Update the database with the latest Scryfall data.

/*
var dbManager = host.Services.GetRequiredService<ScryfallDatabaseManager>();
await dbManager.UpdateDatabaseAsync(true);

var searchParams = new MoxfieldSearchParameters
{
    PageNumber = 1,
    PageSize = 100,
    SortDirection = SortDirection.Descending,
    SortType = SortType.Created,
    AuthorUserName = "GamesfreakSA",
    Format = "commander"
};

var count = 0;

await foreach (var deckId in moxfieldApi.GetDeckIdsAsync(searchParams))
{
    try
    {
        var deck = await moxfieldDownloader.DownloadDeckAsync(deckId);
        await MagicIngestor.InjestDeckAsync(dbContext, deck);
        
        count = (count + 1) % 200;

        if (count == 0)
        {
            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Clear();
        }

        await Task.Delay(1000);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to ingest deck {DeckId}.", deckId);
    }
}

await dbContext.SaveChangesAsync();
dbContext.ChangeTracker.Clear();

await dbContext.UpdateCardCountsAsync();
*/
var mlOptions = host.Services.GetRequiredService<IOptions<MLOptions>>().Value;

using (var file = File.Create(mlOptions.RecommendationPath))
{
    RecommendationTrainer.WriteCardRecModel(dbContext, file);
}
