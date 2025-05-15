using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Core.Processor;

public class Mos6502Cpu : ICpu
{
    public Registers Registers => _reg;

    private readonly Mmu _mmu;
    private readonly Registers _reg;
    private readonly IOpcodeHandler _opcodeHandler;

    public Mos6502Cpu(Mmu mmu)
    {
        _mmu = mmu;

        _reg = new Registers
        {
            A = 0x00,
            X = 0x00,
            Y = 0x00,
            P = 0b0010_0000,
            S = 0xFF,         // Technically programs should do this
            PC = 0xFFFC       // This should probably be loaded from the ROM at this address       
        };

        _opcodeHandler = CreateOpcodeHandler(_reg, _mmu);
    }

    protected virtual IOpcodeHandler CreateOpcodeHandler(Registers reg, Mmu mmu) =>
        new Mos6502OpcodeHandler(reg, mmu);

    public Opcode ExecuteInstruction()
    {
        var opcode = _opcodeHandler.FetchInstruction();
        opcode!.Execute!();
        return opcode;
    }

    public Opcode PeekInstruction(word addr) =>
        _opcodeHandler.PeekInstruction(addr);

    public override string? ToString() =>
        _reg.ToString();
}
