using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Utilities;

namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class OpcodeHandler
{
    // Some variables to carry values between ticks
    byte _lsb;
    byte _msb;
    word _address;

    readonly Registers _reg;
    readonly Mmu _mmu;

    Dictionary<byte, Opcode> _opcodes = new();

    /// <summary>
    /// The absolute address of the last instruction executed
    /// </summary>
    public word Address => _address;

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

    public Opcode PeekInstruction(word addr)
    {        
        Opcode? opcode = GetOpcode(_mmu[addr]);

        if (opcode == null)
            throw new NotImplementedException($"Opcode 0x{_mmu[addr]:X2} does not exist");

        opcode.SetSubstitutions(_mmu, addr);
        return opcode;
    }

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

    void BRK()
    {
        // Push the return address onto the stack
        _mmu[0x0100 + _reg.S] = (byte)(((_reg.PC + 1) >> 8) & 0xFF); // Push high byte of PC
        _reg.S--;
        _mmu[0x0100 + _reg.S] = (byte)((_reg.PC + 1) & 0xFF); // Push low byte of PC
        _reg.S--;

        // Push the processor status onto the stack
        PHP();

        _reg.SetFlag(Flag.Interupt);

        // Set the program counter to the interrupt vector
        _reg.PC = BitUtilities.ToWord(_mmu[0xFFFF], _mmu[0xFFFE]);
    }

    void LDA(byte value)
    {
        _reg.A = value;
        _reg.SetNegativeAndZeroFlags(value);
    }

    void LDX(byte value)
    {
        _reg.X = value;
        _reg.SetNegativeAndZeroFlags(value);
    }

    void LDY(byte value)
    {
        _reg.Y = value;
        _reg.SetNegativeAndZeroFlags(value);
    }

    void STA(word _)
    {
        _mmu[_address] = _reg.A;
    }

    void STX(word _)
    {
        _mmu[_address] = _reg.X;
    }

    void STY(word _)
    {
        _mmu[_address] = _reg.Y;
    }

    void INC(word _)
    {
        byte value = _mmu[_address];
        value++;
        _mmu[_address] = value;
        _reg.SetNegativeAndZeroFlags(value);
    }

    void INX()
    {
        _reg.X++;
        _reg.SetNegativeAndZeroFlags(_reg.X);
    }

    void INY()
    {
        _reg.Y++;
        _reg.SetNegativeAndZeroFlags(_reg.Y);
    }

    void DEC(word _)
    {
        byte value = _mmu[_address];
        value--;
        _mmu[_address] = value;
        _reg.SetNegativeAndZeroFlags(value);
    }

    void DEX()
    {
        _reg.X--;
        _reg.SetNegativeAndZeroFlags(_reg.X);
    }
    void DEY()
    {
        _reg.Y--;
        _reg.SetNegativeAndZeroFlags(_reg.Y);
    }

    void AND(byte value)
    {
        _reg.A &= value;
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void ORA(byte value)
    {
        _reg.A |= value;
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void EOR(byte value)
    {
        _reg.A ^= value;
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void ADC(byte value)
    {
        int carryIn = _reg.GetFlag(Flag.Carry) ? 1 : 0;

        if (_reg.GetFlag(Flag.Decimal))
        {
            // First, raw binary addition for the overflow and negative flags
            int binaryResult = _reg.A + value + carryIn;
            byte binaryResultByte = (byte)binaryResult;

            // Set Overflow flag based on binary result
            bool overflow = (~(_reg.A ^ value) & (_reg.A ^ binaryResultByte) & 0x80) != 0;
            _reg.SetFlag(Flag.Overflow, overflow);

            // Set Negative flag based on binary result
            _reg.SetFlag(Flag.Negative, (binaryResultByte & 0x80) != 0);

            // Set the zero flag based on the binary result
            _reg.SetFlag(Flag.Zero, binaryResultByte == 0);

            // In BCD mode, each nibble represents a decimal digit (0-9)
            int lowNibble = (_reg.A & 0x0F) + (value & 0x0F) + carryIn;
            int highNibble = (_reg.A >> 4) + (value >> 4);

            if (lowNibble > 9)
            {
                lowNibble -= 10;
                highNibble++;
            }

            if (highNibble > 9)
            {
                highNibble -= 10;
                _reg.SetFlag(Flag.Carry);
            }
            else
            {
                _reg.ResetFlag(Flag.Carry);
            }

            _reg.A = (byte)((highNibble << 4) | (lowNibble & 0x0F));
        }
        else
        {
            // Regular binary mode addition
            int result = _reg.A + value + carryIn;

            _reg.SetFlag(Flag.Carry, result > 0xFF);
            byte resultByte = (byte)result;
            _reg.SetNegativeAndZeroFlags(resultByte);

            // Set overflow correctly for binary
            _reg.SetFlag(Flag.Overflow, ((_reg.A ^ resultByte) & (value ^ resultByte) & 0x80) != 0);

            _reg.A = resultByte;
        }
    }

    void SBC(byte value)
    {
        int carryIn = _reg.GetFlag(Flag.Carry) ? 0 : 1; // Remember: Carry clear = borrow happened

        if (_reg.GetFlag(Flag.Decimal)) // Decimal mode
        {
            // Do the binary SBC to set the overflow flag
            int binaryResult = _reg.A - value - carryIn;
            byte binaryResultByte = (byte)binaryResult;

            // Set Overflow flag from binary result
            bool overflow = ((_reg.A ^ value) & 0x80) != 0 && ((_reg.A ^ binaryResultByte) & 0x80) != 0;
            _reg.SetFlag(Flag.Overflow, overflow);

            // Set Negative flag from binary result
            _reg.SetFlag(Flag.Negative, (binaryResultByte & 0x80) != 0);

            // In BCD mode, each nibble represents a decimal digit (0-9)
            int lowNibble = (_reg.A & 0x0F) - (value & 0x0F) - carryIn;
            int highNibble = (_reg.A >> 4) - (value >> 4);

            // Adjust low nibble and borrow from high nibble if needed
            if (lowNibble < 0)
            {
                lowNibble += 10;
                highNibble--;
            }

            // Adjust high nibble if needed
            if (highNibble < 0)
            {
                highNibble += 10;
                _reg.ResetFlag(Flag.Carry); // Borrow happened
            }
            else
            {
                _reg.SetFlag(Flag.Carry); // No borrow
            }

            // Combine high and low nibbles into the final BCD result
            _reg.A = (byte)((highNibble << 4) | (lowNibble & 0x0F));

            // Set the zero flag if the result is zero
            _reg.SetFlag(Flag.Zero, _reg.A == 0);
        }
        else
        {
            // Binary mode: normal SBC logic
            // Perform subtraction with borrow
            int result = _reg.A - value - carryIn;

            // Set Carry flag (inverted logic):
            // Carry is set if NO borrow occurred (i.e., result >= 0)
            _reg.SetFlag(Flag.Carry, result >= 0);

            // Truncate result to 8 bits
            result &= 0xFF;

            // Set Zero flag
            _reg.SetFlag(Flag.Zero, result == 0);

            // Set Negative flag (based on bit 7)
            _reg.SetFlag(Flag.Negative, (result & 0x80) != 0);

            // Set Overflow flag
            // Overflow happens if the sign of A and value differ,
            // and the sign of result differs from A
            _reg.SetFlag(Flag.Overflow, ((_reg.A ^ result) & (_reg.A ^ value) & 0x80) != 0);

            // Store result in A
            _reg.A = (byte)result;
        }
    }

    void CMP(byte value)
    {
        byte result = (byte)(_reg.A - value);
        _reg.SetNegativeAndZeroFlags(result);
        if (_reg.A >= value)
            _reg.SetFlag(Flag.Carry);
        else
            _reg.ResetFlag(Flag.Carry);
    }

    void CPX(byte value)
    {
        byte result = (byte)(_reg.X - value);
        _reg.SetNegativeAndZeroFlags(result);
        if (_reg.X >= value)
            _reg.SetFlag(Flag.Carry);
        else
            _reg.ResetFlag(Flag.Carry);
    }

    void CPY(byte value)
    {
        byte result = (byte)(_reg.Y - value);
        _reg.SetNegativeAndZeroFlags(result);
        if (_reg.Y >= value)
            _reg.SetFlag(Flag.Carry);
        else
            _reg.ResetFlag(Flag.Carry);
    }

    void BIT(byte value)
    {
        byte result = (byte)(_reg.A & value);
        _reg.SetFlag(Flag.Zero, result == 0);
        _reg.SetFlag(Flag.Negative, value.IsBitSet(7));
        _reg.SetFlag(Flag.Overflow, value.IsBitSet(6));
    }

    void Branch(bool condition)
    {
        // Get the signed offset
        byte offset = NextByte();
        if (condition)
        {
            if (offset.IsNegative())
            {
                _reg.PC -= (byte)(~offset + 1); // Two's complement to get the positive offset
            }
            else
            {
                // Add the offset to the current PC
                _reg.PC += (byte)offset;
            }
        }
    }

    void ASL()
    {
        // Shift left the accumulator
        _reg.SetFlag(Flag.Carry, _reg.A.IsBitSet(7));
        _reg.A <<= 1;
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void ASL(word _)
    {
        // Shift left the memory location
        byte value = _mmu[_address];
        _reg.SetFlag(Flag.Carry, value.IsBitSet(7));
        value <<= 1;
        _mmu[_address] = value;
        _reg.SetNegativeAndZeroFlags(value);
    }

    void LSR()
    {
        // Shift right the accumulator
        _reg.SetFlag(Flag.Carry, _reg.A.IsBitSet(0));
        _reg.A >>= 1;
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void LSR(word _)
    {
        // Shift right the memory location
        byte value = _mmu[_address];
        _reg.SetFlag(Flag.Carry, value.IsBitSet(0));
        value >>= 1;
        _mmu[_address] = value;
        _reg.SetNegativeAndZeroFlags(value);
    }

    void ROL()
    {
        // Rotate left the accumulator
        bool carry = _reg.GetFlag(Flag.Carry);
        _reg.SetFlag(Flag.Carry, _reg.A.IsBitSet(7));
        _reg.A = (byte)((_reg.A << 1) | (carry ? 1 : 0));
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void ROL(word _)
    {
        // Rotate left the memory location
        byte value = _mmu[_address];
        bool carry = _reg.GetFlag(Flag.Carry);
        _reg.SetFlag(Flag.Carry, value.IsBitSet(7));
        value = (byte)((value << 1) | (carry ? 1 : 0));
        _mmu[_address] = value;
        _reg.SetNegativeAndZeroFlags(value);
    }

    void ROR()
    {
        // Rotate right the accumulator
        bool carry = _reg.GetFlag(Flag.Carry);
        _reg.SetFlag(Flag.Carry, _reg.A.IsBitSet(0));
        _reg.A = (byte)((_reg.A >> 1) | (carry ? 0x80 : 0));
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void ROR(word _)
    {
        // Rotate right the memory location
        byte value = _mmu[_address];
        bool carry = _reg.GetFlag(Flag.Carry);
        _reg.SetFlag(Flag.Carry, value.IsBitSet(0));
        value = (byte)((value >> 1) | (carry ? 0x80 : 0));
        _mmu[_address] = value;
        _reg.SetNegativeAndZeroFlags(value);
    }

    void JSR(word _)
    {
        // Push the return address onto the stack
        _mmu[0x0100 + _reg.S] = (byte)(((_reg.PC - 1) >> 8) & 0xFF); // Push high byte of PC
        _reg.S--;
        _mmu[0x0100 + _reg.S] = (byte)((_reg.PC - 1) & 0xFF); // Push low byte of PC
        _reg.S--;
        // Jump to the subroutine
        _reg.PC = _address;
    }

    void RTS()
    {
        // Pull the return address from the stack
        _reg.S++;
        _lsb = _mmu[0x0100 + _reg.S]; // Pull high byte of PC
        _reg.S++;
        _msb = _mmu[0x0100 + _reg.S]; // Pull low byte of PC
        // Set the program counter to the return address
        _reg.PC = BitUtilities.ToWord(_msb, _lsb);
        // Increment PC to point to the next instruction
        _reg.PC++;
    }

    void RTI()
    {
        // Pull the processor status from the stack
        PLP();
        // Pull the return address from the stack
        _reg.S++;
        _lsb = _mmu[0x0100 + _reg.S]; // Pull high byte of PC
        _reg.S++;
        _msb = _mmu[0x0100 + _reg.S]; // Pull low byte of PC
        // Set the program counter to the return address
        _reg.PC = BitUtilities.ToWord(_msb, _lsb);
    }

    void JMP(word _)
    {
        _reg.PC = _address;
    }

    void TXS()
    {
        _reg.S = _reg.X;
    }

    void TSX()
    {
        _reg.X = _reg.S;
        _reg.SetNegativeAndZeroFlags(_reg.X);
    }

    void TXA()
    {
        _reg.A = _reg.X;
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void TYA()
    {
        _reg.A = _reg.Y;
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void TAX()
    {
        _reg.X = _reg.A;
        _reg.SetNegativeAndZeroFlags(_reg.X);
    }

    void TAY()
    {
        _reg.Y = _reg.A;
        _reg.SetNegativeAndZeroFlags(_reg.Y);
    }

    void PLA()
    {
        _reg.S++;
        _reg.A = _mmu[0x0100 + _reg.S];
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void PHA()
    {
        _mmu[0x0100 + _reg.S] = _reg.A;
        _reg.S--;
    }

    void PLP()
    {
        _reg.S++;
        _reg.P = (byte)(_mmu[0x0100 + _reg.S] & 0b1110_1111 | 0b0010_0000);
    }

    void PHP()
    {
        _mmu[0x0100 + _reg.S] = _reg.P;
        // Set the unused bits to 1
        _mmu[0x0100 + _reg.S] |= 0x30;
        _reg.S--;
    }
}
