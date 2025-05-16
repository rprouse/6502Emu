using System.Globalization;

namespace Mos6502Emu.Core.Utilities;

public static class ByteExtensions
{
    public static byte ParseHexByte(this string s) =>
      byte.Parse(StripHexIdentifiers(s), NumberStyles.HexNumber, null);

    public static bool TryParseHexByte(this string s, out byte hex) =>
        byte.TryParse(StripHexIdentifiers(s), NumberStyles.HexNumber, null, out hex);

    private static string StripHexIdentifiers(string s)
    {
        if (s.StartsWith("0x"))
            return s.Substring(2);

        if (s.StartsWith("$"))
            return s.Substring(1);

        return s;
    }

    public static string ToBinaryString(this byte b)
    {
        string binary = Convert.ToString(b, 2).PadLeft(8, '0');
        return $"0b{binary.Substring(0, 4)}_{binary.Substring(4, 4)}";
    }

    public static string ToHexString(this byte b) =>
        "0x" + b.ToString("X2", CultureInfo.InvariantCulture).ToUpperInvariant();
}
