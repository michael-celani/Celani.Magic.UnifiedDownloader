using System.ComponentModel.DataAnnotations;

namespace Celani.Magic.Downloader.Storage;

public class StorageOptions
{
    public const string Options = "Storage";

    [Required]
    public required string DbPath { get; set; } = default!;
}
