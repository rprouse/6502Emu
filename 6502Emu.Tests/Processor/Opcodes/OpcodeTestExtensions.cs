using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Tests.Processor.Opcodes;

public static class OpcodeTestExtensions
{
    public static Opcode FetchVerifyAndExecuteInstruction(this Mos6502OpcodeHandler opcodeHandler)
    {
        var op = opcodeHandler.FetchInstruction();
        op.Should().NotBeNull();
        op.Execute.Should().NotBeNull();
        op.Execute();
        return op;
    }
}
