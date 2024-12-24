using System.Text.Json.Serialization;

namespace Celani.Magic.Scryfall;

public class ScryfallCardObject
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("oracle_id")]
    public string? OracleId { get; set; }

    [JsonPropertyName("type_line")]
    public string? TypeLine { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("color_identity")]
    public List<string>? ColorIdentity { get; set; }

    [JsonPropertyName("card_faces")]
    public List<ScryfallCardFaceObject>? CardFaces { get; set; }

    [JsonPropertyName("layout")]
    public required CardLayout Layout { get; set; }

    [JsonPropertyName("mana_cost")]
    public string? ManaCost { get; set; }

    [JsonPropertyName("oracle_text")]
    public string? OracleText { get; set; }

    [JsonIgnore]
    public string? CompositeManaCost => this switch
    {
        { ManaCost: not null } => ManaCost,
        { Layout: CardLayout.Split } => GetCombinedManaCost(this),
        { Layout: CardLayout.Transform } => GetCombinedManaCost(this),
        { Layout: CardLayout.ModalDoubleFaced } => GetCombinedManaCost(this),
        { Layout: CardLayout.Adventure } => GetCombinedManaCost(this),
        { Layout: CardLayout.DoubleFacedToken } => GetCombinedManaCost(this),
        { Layout: CardLayout.Reversible } => CardFaces?.First().ManaCost,
        _ => null
    };

    [JsonIgnore]
    public string? CompositeOracleText => this switch
    {
        { OracleText: not null } => OracleText,
        { Layout: CardLayout.Split } => GetCombinedOracleText(this),
        { Layout: CardLayout.Flip } => GetCombinedOracleText(this),
        { Layout: CardLayout.Transform } => GetCombinedOracleText(this),
        { Layout: CardLayout.ModalDoubleFaced } => GetCombinedOracleText(this),
        { Layout: CardLayout.Adventure } => GetCombinedOracleText(this),
        { Layout: CardLayout.DoubleFacedToken } => GetCombinedOracleText(this),
        { Layout: CardLayout.Reversible } => CardFaces?.First().OracleText,
        _ => null
    };

    [JsonIgnore]
    public string CompositeTypeLine => TypeLine ?? CardFaces?.FirstOrDefault()?.TypeLine ?? "";

    [JsonIgnore]
    public string CompositeOracleId => OracleId ?? CardFaces?.FirstOrDefault()?.OracleId ?? throw new InvalidOperationException("Card object contains no Oracle ID.");

    private static string? GetCombinedManaCost(ScryfallCardObject co)
    {
        var combined = string.Join(" // ", co.CardFaces!.Select(x => x.ManaCost).Where(x => !string.IsNullOrEmpty(x)));

        return !string.IsNullOrEmpty(combined) ? combined : null;
    }

    private static string? GetCombinedOracleText(ScryfallCardObject co)
    {
        var combined = string.Join(" // ", co.CardFaces!.Select(x => x.OracleText).Where(x => !string.IsNullOrEmpty(x)));

        return !string.IsNullOrEmpty(combined) ? combined : null;
    }
}
