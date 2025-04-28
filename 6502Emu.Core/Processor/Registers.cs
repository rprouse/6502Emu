using Mos6502Emu.Core.Utilities;

namespace Mos6502Emu.Core.Processor;

public class Registers
{
    /// <summary>
    /// The Accumulator register
    /// </summary>
    public byte A { get; set; }

    /// <summary>
    /// The X index register
    /// </summary>
    public byte X { get; set; }

    /// <summary>
    /// The Y index register
    /// </summary>
    public byte Y { get; set; }

    /// <summary>
    /// The Stack Pointer register
    /// </summary>
    public byte S { get; set; }

    /// <summary>
    /// The Processor Status register
    /// </summary>
    public byte P { get; set; }

    /// <summary>
    /// The Program Counter register
    /// </summary>
    public word PC { get; set; }

    public bool GetFlag(Flag flag) =>
        (P & (byte)flag) != 0;

    public void SetFlag(Flag flag, bool set)
    {
        if (set)
            SetFlag(flag);
        else
            ResetFlag(flag);
    }

    public void SetFlag(Flag flag) =>
        P = (byte)(P | (byte)flag);

    public void ResetFlag(Flag flag) =>
        P = (byte)(P & ~(byte)flag);

    public void SetNegativeAndZeroFlags(byte value)
    {
        SetFlag(Flag.Zero, value == 0);
        SetFlag(Flag.Negative, value.IsNegative());
    }
}
