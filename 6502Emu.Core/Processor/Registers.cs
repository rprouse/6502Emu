using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    // Bit 7: Negative Flag
    // Bit 6: Overflow Flag
    // Bit 5: Not Used
    // Bit 4: Break Instruction Flag
    // Bit 3: Decimal Mode Flag
    // Bit 2: IRQ Disable Flag
    // Bit 1: Zero Flag
    // Bit 0: Carry Flag
    public enum Flag : byte
    {
        Negative = 0b1000_0000,
        Overflow = 0b0100_0000,
        Break = 0b0001_0000,
        Decimal = 0b0000_1000,
        IrqDisable = 0b0000_0100,
        Zero = 0b0000_0010,
        Carry = 0b0000_0001
    }

    public bool GetFlag(Flag flag) =>
        (P & (byte)flag) != 0;

    public void SetFlag(Flag flag) =>
        P = (byte)(P | (byte)flag);

    public void ResetFlag(Flag flag) =>
        P = (byte)(P & ~(byte)flag);
}
