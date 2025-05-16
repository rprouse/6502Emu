using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Utilities;

namespace Mos6502Emu.Core.Processor.Opcodes;

/// <summary>
/// 6502 Opcode class.
/// </summary>
/// <param name="mnemonic">The mnemonic</param>
/// <param name="addr_mode">The address mode</param>
/// <param name="hex">Hex value of the opcode</param>
/// <param name="length">Length of the instruction in bytes</param>
/// <param name="description">A brief description of the opcode</param>
public class Opcode(string mnemonic, string addr_mode, byte hex, byte length, string description)
{
    // Substitution values
    byte? _n;
    sbyte? _d;
    word? _nn;

    string _mnemonic = mnemonic;
    string _description = description;

    public string Mnemonic
    {
        get
        {
            string operand = AddressMode switch 
            {
                "Immediate" => $"#${_n:X2}",
                "Zero Page" => $"${_n:X2}",
                "Zero Page,X" => $"${_n:X2},X",
                "Zero Page,Y" => $"${_n:X2},Y",
                "(Indirect,X)" => $"(${_n:X2},X)",
                "(Indirect),Y" => $"(${_n:X2}),Y",
                "Relative" => $"${_d:X2}",
                "Absolute" => $"${_nn:X4}",
                "Absolute,X" => $"${_nn:X4},X",
                "Absolute,Y" => $"${_nn:X4},Y",
                "Indirect" => $"(${_nn:X4})",
                "Implied" => string.Empty,
                "Accumulator" => string.Empty,
                // W65C02S specific address modes
                "(Zero Page)" => $"(${_n:X2})",
                "(Absolute,X)" => $"${_nn:X4},X",
                "Zero Page, Relative" => $"${_n:X2},${_d:X2}",
                _ => throw new ArgumentOutOfRangeException($"Unknown addressing mode: {AddressMode} for opcode {_mnemonic}")
            };
            return $"{_mnemonic} {operand}".Trim();
        }
    }

    public string AddressMode { get; } = addr_mode;

    public byte Hex { get; } = hex;

    /// <summary>
    /// This is the full length of the opcode including the operands
    /// </summary>
    public byte Length { get; } = length;

    public string Description => $"{_description} {AddressMode}";

    public Action? Execute { get; set; }

    public void SetSubstitutions(Mmu mmu, word addr)
    {
        _n = null;
        _d = null;
        _nn = null;

        switch (AddressMode)
        {
            case "Immediate":
            case "Zero Page":
            case "Zero Page,X":
            case "Zero Page,Y":
            case "(Indirect,X)":
            case "(Indirect),Y":
            case "(Zero Page)":
                _n = mmu[addr + 1];
                break;
            case "Relative":
                _d = (sbyte)mmu[addr + 1];
                break;
            case "Absolute":
            case "Absolute,X":
            case "Absolute,Y":
            case "Indirect":
            case "(Absolute,X)":
                _nn = BitUtilities.ToWord(mmu[addr + 2], mmu[addr + 1]);
                break;
            case "Implied":
            case "Accumulator":
                break;
            case "Zero Page, Relative":
                _n = mmu[addr + 1];
                _d = (sbyte)mmu[addr + 2];
                break;
            default:
                throw new ArgumentOutOfRangeException($"Unknown addressing mode: {AddressMode} for opcode {_mnemonic}");
        }
    }

    public override string ToString() => $"{Mnemonic} {AddressMode} ; {Description}";
}
