namespace Mos6502Emu.Core.Processor.Opcodes;

/// <summary>
/// 6502 Opcode class.
/// </summary>
/// <param name="mnemonic">The mnemonic</param>
/// <param name="addr_mode">The addess mode</param>
/// <param name="hex">Hex value of the opcode</param>
/// <param name="length">Length of the instruction in bytes</param>
/// <param name="description">A brief description of the opcode</param>
public class Opcode(string mnemonic, string addr_mode, byte hex, byte length, string description)
{
    public string Mnemonic { get; } = mnemonic;
    public string AddressMode { get; } = addr_mode;
    public byte Hex { get; } = hex;
    public byte OpcodeLength { get; } = length;
    public string Description { get; } = description;

    public Action? Execute { get; set; }

    public override string ToString() => $"{Mnemonic} {AddressMode} ; {Description}";
}
