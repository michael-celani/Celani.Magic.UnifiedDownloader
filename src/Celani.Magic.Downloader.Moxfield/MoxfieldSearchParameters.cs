using Refit;
using System.Runtime.Serialization;

namespace Celani.Magic.Downloader.Moxfield;

public enum SortType
{
    [EnumMember(Value = "updated")]
    Updated,

    [EnumMember(Value = "created")]
    Created,
}

public enum SortDirection
{
    [EnumMember(Value = "ascending")]
    Ascending,

    [EnumMember(Value = "descending")]
    Descending
}

public class MoxfieldSearchParameters
{
    [AliasAs("pageNumber")]
    public int PageNumber { get; set; }

    [AliasAs("pageSize")]
    public int PageSize { get; set; }

    [AliasAs("sortType")]
    public SortType SortType { get; set; }

    [AliasAs("sortDirection")]
    public SortDirection SortDirection { get; set; }

    [AliasAs("commanderCardId")]
    public string? CommanderCardId { get; set; }

    [AliasAs("partnerCardId")]
    public string? PartnerCardId { get; set; }

    [AliasAs("fmt")]
    public string? Format { get; set; }

    [AliasAs("authorUserNames")]
    public string? AuthorUserName { get; set; }
}