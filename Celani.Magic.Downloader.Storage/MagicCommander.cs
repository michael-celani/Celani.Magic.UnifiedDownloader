using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Celani.Magic.Downloader.Storage;

[Index(nameof(CommanderId), nameof(PartnerId), IsUnique = true)]
public record MagicCommander
{
    public int Id { get; set; }

    public int CommanderId { get; set; }

    public OracleCard Commander { get; set; } = null!;

    public int? PartnerId { get; set; }

    public OracleCard? Partner { get; set; }

    public List<StoredDeck>? Decks { get; set; }

    [NotMapped]
    public ColorIdentity ColorIdentity => (Commander?.ColorIdentity ?? 0) | (Partner?.ColorIdentity ?? 0);
}