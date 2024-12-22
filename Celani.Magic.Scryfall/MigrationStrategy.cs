using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public enum MigrationStrategy
{
    [JsonStringEnumMemberName("merge")]
    Merge,

    [JsonStringEnumMemberName("delete")]
    Delete
}
