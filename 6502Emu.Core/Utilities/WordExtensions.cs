using System.Globalization;

namespace Mos6502Emu.Core.Utilities;

public static class WordExtensions
{
    public static word ParseHexWord(this string s) =>
        word.Parse(StripHexIdentifiers(s), NumberStyles.HexNumber, null);

    public static bool TryParseHexWord(this string s, out word hex) =>
        word.TryParse(StripHexIdentifiers(s), NumberStyles.HexNumber, null, out hex);

    private static string StripHexIdentifiers(string s)
    {
        if (s.StartsWith("0x"))
            return s.Substring(2);

        if (s.StartsWith("$"))
            return s.Substring(1);

        return s;
    }

    public static string ToHexString(this word value) =>
        "0x" + value.ToString("X4", CultureInfo.InvariantCulture);
}
