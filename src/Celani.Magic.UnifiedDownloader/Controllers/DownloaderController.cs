using Celani.Magic.Downloader.Core;
using Celani.Magic.UnifiedDownloader.Api;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Celani.Magic.UnifiedDownloader.Web.Controllers;

[ApiController]
public class DownloaderController(IEnumerable<IMagicDownloader> downloaders) : ControllerBase
{
    private Dictionary<string, IMagicDownloader> Downloaders { get; } = downloaders.ToDictionary(x => x.Backend);

    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("/api/decks/{backend}/{id}")]
    public async Task<ActionResult<DownloadResult>> GetAsync(WebsiteBackend backend, string id)
    {
        // Get the string representation of the backend:
        var backendStr = Enum.GetName(backend)!.ToLowerInvariant();

        // See if we have it:
        if (!Downloaders.TryGetValue(backendStr, out var downloader))
        {
            var err = new DownloadResult
            {
                ResultCode = DownloadResultCode.ErrorNotFound,
            };

            return NotFound(err);
        }

        DownloadedMagicList deck;

        // Download the deck:
        try
        {
            deck = await downloader.DownloadDeckAsync(id);
        }
        catch (Refit.ApiException ex)
        {
            var err = new DownloadResult
            {
                ResultCode = ex.StatusCode == System.Net.HttpStatusCode.NotFound ? 
                    DownloadResultCode.ErrorNotFound : 
                    DownloadResultCode.ErrorUnknown,
                    
                Error = new DownloadResultFailure()
                {
                    Messages = [ex.Message],
                }
            };

            return BadRequest(err);
        }

        // Transform the downloaded deck to the API:
        var apiDeck = new MagicDeck
        {
            Id = deck.Id,
            Backend = backend,
            Name = deck.Name,
            Mainboard = ToApiIncludeList(deck.Mainboard),
            Sideboard = ToApiIncludeList(deck.Sideboard),
            Maybeboard = ToApiIncludeList(deck.Maybeboard),
            Commanders = ToApiIncludeList(deck.Commanders),
            Companions = ToApiIncludeList(deck.Companions),
            SignatureSpells = ToApiIncludeList(deck.SignatureSpells),
            Attractions = ToApiIncludeList(deck.Attractions),
            Stickers = ToApiIncludeList(deck.Stickers),
            Contraptions = ToApiIncludeList(deck.Contraptions),
            Planes = ToApiIncludeList(deck.Planes),
            Schemes = ToApiIncludeList(deck.Schemes),
            Tokens = ToApiIncludeList(deck.Tokens)
        };

        var res = new DownloadResult
        {
            ResultCode = DownloadResultCode.Success,

            Data = new DownloadResultSuccess
            {
                Deck = apiDeck
            }
        };

        return Ok(res);
    }

    private static IList<MagicInclude> ToApiIncludeList(IList<DownloadedMagicInclude>? list)
    {
        return list is not null ? [.. list.Select(ToApiInclude)] : [];
    }

    private static MagicInclude ToApiInclude(DownloadedMagicInclude include)
    {
        return new MagicInclude
        {
            Quantity = include.Quantity,
            ScryfallId = include.ScryfallId,
            ScryfallOracleId = include.ScryfallOracleId
        };
    }
}
