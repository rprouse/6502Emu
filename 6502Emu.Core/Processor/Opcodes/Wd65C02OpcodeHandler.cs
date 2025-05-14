using Mos6502Emu.Core.Memory;

namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class Wd65C02OpcodeHandler : Mos6502OpcodeHandler
{
    public Wd65C02OpcodeHandler(Registers registers, Mmu mmu) : base(registers, mmu)
    {
    }
}
