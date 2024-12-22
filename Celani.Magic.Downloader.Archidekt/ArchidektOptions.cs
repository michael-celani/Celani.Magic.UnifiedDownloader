using System.ComponentModel.DataAnnotations;

namespace Celani.Magic.Downloader.Archidekt;

internal class ArchidektOptions
{
    public const string Options = "Backend:Archidekt";

    [Required]
    public required Uri ArchidektApiUri { get; set; } = default!;
}
