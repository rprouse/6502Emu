namespace Mos6502Emu.Tests.Processor.Opcodes;

// Represents the state of the CPU registers, flags and memory
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
    public word[][] RAM { get; set; }
}
