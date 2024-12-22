using System.ComponentModel.DataAnnotations;

namespace Celani.Magic.ML;

public class MLOptions
{
    public const string Options = "Storage:Models";

    [Required]
    public required string RecommendationPath { get; set; } = default!;
}
