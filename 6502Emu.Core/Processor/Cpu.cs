using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Core.Processor;

public class Cpu
{
    public Registers Registers => _reg;

    private readonly Mmu _mmu;
    private readonly Registers _reg;
    private readonly OpcodeHandler _opcodeHandler;

    public Cpu(Mmu mmu)
    {
        _mmu = mmu;

        _reg = new Registers
        {
            A = 0x00,
            X = 0x00,
            Y = 0x00,
            P = 0b0010_0000,
            S = 0xFF,         // Technically programs should do this
            PC = 0xFFFC     // This should probably be loaded from the ROM at this address       
        };

        _opcodeHandler = new OpcodeHandler(_reg, _mmu);
    }

    public Opcode Tick()
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
