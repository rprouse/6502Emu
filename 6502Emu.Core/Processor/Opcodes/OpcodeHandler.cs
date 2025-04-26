using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Utilities;

namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class OpcodeHandler
{
    // Some variables to carry values between ticks
    byte _lsb;
    byte _msb;
    byte _operand;
    word _address;

    readonly Registers _reg;
    readonly Mmu _mmu;

    Dictionary<byte, Opcode> _opcodes = new ();

    public OpcodeHandler(Registers registers, Mmu mmu)
    {
        _reg = registers;
        _mmu = mmu;
        IntializeOpcodes();
        InitializeMethods();
    }

    public Opcode FetchInstruction()
    {
        Opcode opcode = PeekInstruction(_reg.PC);

        // Consume the initial opcode byte. Operands are consumed by the opcode execution.
        _reg.PC++;

        return opcode;
    }

    public Opcode GetOpcode(byte hex)
    {
        if (!_opcodes.TryGetValue(hex, out Opcode? opcode))
            throw new NotImplementedException($"Opcode 0x{hex:X2} does not exist");
        return opcode;
    }

    public Opcode PeekInstruction(word addr) => GetOpcode(_mmu[addr]);

    private void Add(Opcode opcode)
    {
        _opcodes.Add(opcode.Hex, opcode);
    }

    /// <summary>
    /// Reads the next byte from memory and increments PC
    /// </summary>
    /// <returns></returns>
    byte NextByte() => _mmu[_reg.PC++];

    /// <summary>
    /// Reads the next word from memory and increments PC
    /// </summary>
    /// <returns></returns>
    word NextWord()
    {
        _lsb = NextByte();
        _msb = NextByte();
        return BitUtilities.ToWord(_msb, _lsb);
    }

    void NOP() { }
}
