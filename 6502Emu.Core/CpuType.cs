namespace Mos6502Emu.Core;

/// <summary>
/// Represents the set of CPU architectures supported by the system.
/// </summary>
/// <remarks>This enumeration defines the supported CPU types, such as the MOS 6502 and the WDC 65C02. Use this
/// enumeration to specify or identify the CPU architecture in relevant operations.</remarks>
public enum CpuType
{
    MOS6502,
    WD65C02
}
