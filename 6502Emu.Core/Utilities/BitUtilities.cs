namespace Mos6502Emu.Core.Utilities;

public static class BitUtilities
{
    /// <summary>
    /// Least significant bit
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static byte Lsb(this word value) =>
        (byte)(value & 0x00FF);

    /// <summary>
    /// Most significant bit
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static byte Msb(this word value) =>
        (byte)((value >> 8) & 0x00FF);

    /// <summary>
    /// Combine an MSB and an LSB into a Word
    /// </summary>
    /// <param name="msb"></param>
    /// <param name="lsb"></param>
    /// <returns></returns>
    public static word ToWord(byte msb, byte lsb) =>
        (word)(msb << 8 | lsb);

    /// <summary>
    /// Get the value of a bit in a byte
    /// </summary>
    /// <param name="value"></param>
    /// <param name="bit"></param>
    /// <returns></returns>
    public static bool IsBitSet(this byte value, int bit) =>
        (value & (1 << bit)) != 0;

    /// <summary>
    /// Set a bit in a byte
    /// </summary>
    /// <param name="value"></param>
    /// <param name="bit"></param>
    /// <returns></returns>
    public static byte SetBit(this byte value, int bit) =>
        (byte)(value | (1 << bit));

    /// <summary>
    /// Reset a bit in a byte
    /// </summary>
    /// <param name="value"></param>
    /// <param name="bit"></param>
    /// <returns></returns>
    public static byte ResetBit(this byte value, int bit) =>
        (byte)(value & ~(1 << bit));

    /// <summary>
    /// Tests if a signed byte is negative.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNegative(this byte value) => (value & 0x80) != 0;

    /// <summary>
    /// Tests if a signed byte is negative. This version takes an int for the
    /// cases where we are working with the overflow results.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNegative(this int value) => ((byte)value).IsNegative();
}
