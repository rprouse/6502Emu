using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Core.Processor;

public class W65C02SCpu : Mos6502Cpu
{
    public W65C02SCpu(Mmu mmu) : base(mmu)
    {
    }

    protected override IOpcodeHandler CreateOpcodeHandler(Registers reg, Mmu mmu) =>
        new W65C02SOpcodeHandler(reg, mmu);
}
