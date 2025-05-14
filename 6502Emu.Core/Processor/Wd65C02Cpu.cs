using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Core.Processor;

public class Wd65C02Cpu : Mos6502Cpu
{
    public Wd65C02Cpu(Mmu mmu) : base(mmu)
    {
    }

    protected override IOpcodeHandler CreateOpcodeHandler(Registers reg, Mmu mmu) =>
        new Mos6502OpcodeHandler(reg, mmu);
}
