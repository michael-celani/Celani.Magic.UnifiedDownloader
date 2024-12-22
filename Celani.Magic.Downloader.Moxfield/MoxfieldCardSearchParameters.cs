using Refit;

namespace Celani.Magic.Downloader.Moxfield;

public class MoxfieldCardSearchParameters
{
    [AliasAs("q")]
    public required string Query {get; set;}

    [AliasAs("page")]
    public int PageNumber { get; set; }
}