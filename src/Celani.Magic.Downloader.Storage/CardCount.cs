using Microsoft.EntityFrameworkCore;

namespace Celani.Magic.Downloader.Storage;

[Index(nameof(CardId), IsUnique = true)]
public record CardCount
{
    public int Id { get; set; }

    public int CardId { get; set; }

    public OracleCard Card { get; set; } = null!;

    public int Count { get; set; }
}