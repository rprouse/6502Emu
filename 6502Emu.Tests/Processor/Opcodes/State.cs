using System.Text;

namespace Mos6502Emu.Tests.Processor.Opcodes;

// Represents the state of the CPU _registers, flags and memory
public class State
{
    // Program Counter
    public word PC { get; set; }

    // The Stack Pointer register
    public byte S { get; set; }

    // The Accumulator register
    public byte A { get; set; }

    // The X index register
    public byte X { get; set; }

    // The Y index register
    public byte Y { get; set; }

    // The Processor Status register
    public byte P { get; set; }

    // Contains a list of values to store in memory prior to execution, each one in the form `[address, value]`
    public word[][] RAM { get; set; } = Array.Empty<word[]>();

    // Get RAM value by address
    public byte this[word address] =>
        (byte)(RAM.FirstOrDefault(r => r[0] == address)?[1] ?? 0x00);

    public override string ToString()
    {
        StringBuilder sb = new ();

        sb.AppendLine($"PC: 0x{PC:X4} S: 0x{S:X2} A: 0x{A:X2} X: 0x{X:X2} Y: 0x{Y:X2} P: 0x{P:X2}");
        sb.AppendLine("RAM:");
        foreach (var ram in RAM)
        {
            sb.AppendLine($"  0x{ram[0]:X4}: 0x{ram[1]:X2}");
        }
        return sb.ToString();
    }
}
