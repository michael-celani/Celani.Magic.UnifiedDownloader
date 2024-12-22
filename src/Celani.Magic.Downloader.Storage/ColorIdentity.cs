namespace Celani.Magic.Downloader.Storage;

[Flags]
public enum ColorIdentity
{
    None = 0,
    White = 1,
    Blue = 2,
    Black = 4,
    Red = 8,
    Green = 16
}

public static class ColorIdentityExtensions
{
    public static string ToColorString(this ColorIdentity colorIdentity)
    {
        var colorString = string.Empty;

        if (colorIdentity.HasFlag(ColorIdentity.White))
            colorString += "W";
        if (colorIdentity.HasFlag(ColorIdentity.Blue))
            colorString += "U";
        if (colorIdentity.HasFlag(ColorIdentity.Black))
            colorString += "B";
        if (colorIdentity.HasFlag(ColorIdentity.Red))
            colorString += "R";
        if (colorIdentity.HasFlag(ColorIdentity.Green))
            colorString += "G";

        return colorString;
    }
}

public static class ColorIdentityTools
{
    public static ColorIdentity FromColors(IEnumerable<string> colors)
    {
        ColorIdentity result = ColorIdentity.None;

        foreach (var color in colors)
        {
            result |= color.ToUpper() switch
            {
                "W" => ColorIdentity.White,
                "WHITE" => ColorIdentity.White,
                "U" => ColorIdentity.Blue,
                "BLUE" => ColorIdentity.Blue,
                "B" => ColorIdentity.Black,
                "BLACK" => ColorIdentity.Black,
                "R" => ColorIdentity.Red,
                "RED" => ColorIdentity.Red,
                "G" => ColorIdentity.Green,
                "GREEN" => ColorIdentity.Green,
                _ => ColorIdentity.None
            };
        }

        return result;
    }
}