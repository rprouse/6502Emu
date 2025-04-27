namespace Mos6502Emu.Tests.Processor.Opcodes;

public class OpcodeTest
{
    // Used to identify the test
    public required string Name { get; set; }

    // The initial state of the processor
    public required State Initial { get; set; }

    // The state of the processor and relevant memory contents after execution
    public required State Final { get; set; }

    // Provides a cycle-by-cycle breakdown of bus activity in the form `[address, value, type]` where `type` is either `read` or `write`
    public required object[][] Cycles { get; set; }

    public override string ToString() => Name;
}
