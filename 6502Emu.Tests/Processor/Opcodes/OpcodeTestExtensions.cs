using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Tests.Processor.Opcodes;

public static class OpcodeTestExtensions
{
    public static Opcode FetchVerifyAndExecuteInstruction(this IOpcodeHandler opcodeHandler)
    {
        var op = opcodeHandler.FetchInstruction();
        op.ShouldNotBeNull();
        op.Execute.ShouldNotBeNull();
        op.Execute();
        return op;
    }
}
