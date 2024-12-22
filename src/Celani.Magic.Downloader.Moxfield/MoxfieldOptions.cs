using System.ComponentModel.DataAnnotations;

namespace Celani.Magic.Downloader.Moxfield;
internal class MoxfieldOptions
{
    public const string Options = "Backend:Moxfield";

    [Required]
    public required Uri MoxfieldApiUri { get; set; } = default!;

    [Required]
    public required string MoxfieldApiKey { get; set; } = default!;
}
