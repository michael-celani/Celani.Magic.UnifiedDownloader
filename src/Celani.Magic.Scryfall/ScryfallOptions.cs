using System.ComponentModel.DataAnnotations;

namespace Celani.Magic.Scryfall;

public class ScryfallOptions
{
    public const string Options = "Backend:Scryfall";

    [Required]
    public required Uri ScryfallApiUri { get; set; } = default!;
}
