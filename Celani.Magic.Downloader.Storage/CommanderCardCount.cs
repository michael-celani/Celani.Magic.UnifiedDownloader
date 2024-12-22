using Microsoft.EntityFrameworkCore;

namespace Celani.Magic.Downloader.Storage;

[Index(nameof(CommanderId), nameof(CardId), IsUnique = true)]
public record CommanderCardCount
{
    public int Id { get; set; }

    public int CommanderId { get; set; }

    public MagicCommander Commander { get; set; } = null!;
    
    public int CardId { get; set; }

    public OracleCard Card { get; set; } = null!;

    public int Count { get; set; }
}