using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;

namespace Mos6502Emu.Core;

public static class CpuFactory
{
    public static ICpu CreateCpu(this CpuType cpuType, Mmu mmu)
    {
        return cpuType switch
        {
            CpuType.MOS6502 => new Mos6502Cpu(mmu),
            CpuType.W65C02S => new W65C02SCpu(mmu),
            _ => throw new NotSupportedException($"CPU type {cpuType} is not supported.")
        };
    }
}
