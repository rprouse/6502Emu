namespace Mos6502Emu.Core.Processor;

/// <summary>
/// Status register flags
/// </summary>
/// <remarks>
/// Bit 7: Negative Flag
/// Bit 6: Overflow Flag
/// Bit 5: Not Used
/// Bit 4: Break Instruction Flag
/// Bit 3: Decimal Mode Flag
/// Bit 2: IRQ Disable Flag
/// Bit 1: Zero Flag
/// Bit 0: Carry Flag
/// </remarks>
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
