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

    void BRK()
    {
        _reg.SetFlag(Flag.Break);
        _reg.SetFlag(Flag.Interupt);
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
        // Check if the Decimal flag is set
        if (_reg.GetFlag(Flag.Decimal))
            AddWithCarryBDC(value);
        else
            AddWithCarryNonBDC(value);
    }

    void AddWithCarryBDC(byte value)
    {
        // In decimal mode, we need to handle each nibble as a decimal digit (0-9)
        int carryIn = _reg.GetFlag(Flag.Carry) ? 1 : 0;

        // Save original A value to calculate overflow later
        byte originalA = _reg.A;

        // Calculate result with binary addition first
        int binarySum = _reg.A + value + carryIn;

        // Decimal adjust the low nibble (0-9)
        int lowNibbleSum = (_reg.A & 0x0F) + (value & 0x0F) + carryIn;
        if (lowNibbleSum > 9)
        {
            lowNibbleSum += 6; // Add 6 to adjust to BCD
        }

        // Decimal adjust the high nibble (0-9)
        int highNibbleSum = (_reg.A >> 4) + (value >> 4);
        if (lowNibbleSum > 0x0F)
        {
            highNibbleSum++; // Add carry from low nibble
        }

        // Set carry flag
        if (highNibbleSum > 9)
        {
            highNibbleSum += 6; // Add 6 to adjust to BCD
            _reg.SetFlag(Flag.Carry);
        }
        else
        {
            _reg.ResetFlag(Flag.Carry);
        }

        // Combine high and low nibbles
        byte result = (byte)((highNibbleSum << 4) | (lowNibbleSum & 0x0F));

        // Set negative and zero flags
        _reg.SetNegativeAndZeroFlags(result);

        // Set overflow flag (note: overflow is set based on the binary addition)
        // Overflow occurs when both inputs have the same sign but the result has a different sign
        if (((originalA ^ binarySum) & (value ^ binarySum) & 0x80) != 0)
            _reg.SetFlag(Flag.Overflow);
        else
            _reg.ResetFlag(Flag.Overflow);

        _reg.A = result;
    }

    void AddWithCarryNonBDC(byte value)
    {
        // In binary mode, addition is straightforward
        int carryIn = _reg.GetFlag(Flag.Carry) ? 1 : 0;
        int result = _reg.A + value + carryIn;

        // Set carry flag if result exceeds 8-bit range
        if (result > 0xFF)
            _reg.SetFlag(Flag.Carry);
        else
            _reg.ResetFlag(Flag.Carry);

        // Set overflow flag
        // Overflow occurs when both inputs have the same sign but the result has a different sign
        if (((_reg.A ^ result) & (value ^ result) & 0x80) != 0)
            _reg.SetFlag(Flag.Overflow);
        else
            _reg.ResetFlag(Flag.Overflow);

        // Set negative and zero flags based on the result
        byte finalResult = (byte)result;
        _reg.SetNegativeAndZeroFlags(finalResult);

        _reg.A = finalResult;
    }

    void SBC(byte value)
    {
        // Check if the Decimal flag is set
        if (_reg.GetFlag(Flag.Decimal))
            SubtractWithCarryBDC(value);
        else
            SubtractWithCarryNonBDC(value);
    }

    void SubtractWithCarryBDC(byte value)
    {
        // In decimal mode, we need to handle each nibble as a decimal digit (0-9)
        int carryIn = _reg.GetFlag(Flag.Carry) ? 0 : 1; // Note: Carry flag is inverted for subtraction

        // Save original A value to calculate overflow later
        byte originalA = _reg.A;

        // Calculate binary result for overflow detection
        int binaryResult = _reg.A - value - carryIn;

        // Perform BCD subtraction for the low nibble
        int lowNibbleResult = (_reg.A & 0x0F) - (value & 0x0F) - carryIn;
        if (lowNibbleResult < 0)
        {
            lowNibbleResult += 10; // Borrow from high nibble
            lowNibbleResult &= 0x0F; // Keep only lower 4 bits
            carryIn = 1; // Indicate borrow for high nibble
        }
        else
        {
            lowNibbleResult &= 0x0F; // Keep only lower 4 bits
            carryIn = 0; // No borrow needed
        }

        // Perform BCD subtraction for the high nibble
        int highNibbleResult = (_reg.A >> 4) - (value >> 4) - carryIn;
        if (highNibbleResult < 0)
        {
            highNibbleResult += 10; // Apply decimal adjustment
            _reg.ResetFlag(Flag.Carry); // Set borrow (clear carry)
        }
        else
        {
            _reg.SetFlag(Flag.Carry); // No borrow needed (set carry)
        }

        // Combine the high and low nibbles
        byte result = (byte)((highNibbleResult << 4) | lowNibbleResult);

        // Set negative and zero flags
        _reg.SetNegativeAndZeroFlags(result);

        // Set overflow flag (calculated based on binary result)
        // Overflow occurs when the sign of the result is different from what we'd expect
        if (((originalA ^ value) & (originalA ^ binaryResult) & 0x80) != 0)
            _reg.SetFlag(Flag.Overflow);
        else
            _reg.ResetFlag(Flag.Overflow);

        _reg.A = result;
    }

    void SubtractWithCarryNonBDC(byte value)
    {
        // For binary subtraction, the carry flag acts as a "not borrow" flag
        int carryIn = _reg.GetFlag(Flag.Carry) ? 0 : 1; // 1 = borrow, 0 = no borrow
        int result = _reg.A - value - carryIn;

        // Set carry flag when no borrow was needed
        if (result >= 0)
            _reg.SetFlag(Flag.Carry);
        else
            _reg.ResetFlag(Flag.Carry);

        // Set overflow flag when the sign changes unexpectedly
        // This happens when subtracting a positive from negative gives positive,
        // or when subtracting a negative from positive gives negative
        if ((((_reg.A ^ value) & (_reg.A ^ (byte)result)) & 0x80) != 0)
            _reg.SetFlag(Flag.Overflow);
        else
            _reg.ResetFlag(Flag.Overflow);

        // Set negative and zero flags based on the result
        byte finalResult = (byte)result;
        _reg.SetNegativeAndZeroFlags(finalResult);

        _reg.A = finalResult;
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
        _reg.SetNegativeAndZeroFlags(result);
        if ((value & 0x40) != 0)
            _reg.SetFlag(Flag.Overflow);
        else
            _reg.ResetFlag(Flag.Overflow);
    }

    void Branch(bool condition)
    {
        // Get the signed offset
        sbyte offset = (sbyte)NextByte();
        if (condition)
        {
            // Add the offset to the current PC
            _reg.PC += (byte)offset;
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

    void JSR(word address)
    {
        // Push the return address onto the stack
        _reg.S--;
        _mmu[_reg.S] = (byte)((_reg.PC >> 8) & 0xFF); // Push high byte of PC
        _reg.S--;
        _mmu[_reg.S] = (byte)(_reg.PC & 0xFF); // Push low byte of PC
        // Jump to the subroutine
        _reg.PC = address;
    }

    void RTS()
    {
        // Pull the return address from the stack
        _reg.S++;
        _msb = _mmu[_reg.S]; // Pull high byte of PC
        _reg.S++;
        _lsb = _mmu[_reg.S]; // Pull low byte of PC
        // Set the program counter to the return address
        _reg.PC = BitUtilities.ToWord(_msb, _lsb);
        // Increment PC to point to the next instruction
        _reg.PC++;
    }

    void RTI()
    {
        // Pull the processor status from the stack
        _reg.S++;
        _reg.P = _mmu[_reg.S]; // Pull P register
        // Pull the return address from the stack
        _reg.S++;
        _msb = _mmu[_reg.S]; // Pull high byte of PC
        _reg.S++;
        _lsb = _mmu[_reg.S]; // Pull low byte of PC
        // Set the program counter to the return address
        _reg.PC = BitUtilities.ToWord(_msb, _lsb);
    }

    void JMP(word address)
    {
        _reg.PC = address;
    }

    void TXS()
    {
        _reg.S = _reg.X;
        _reg.SetNegativeAndZeroFlags(_reg.S);
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
        _reg.A = _mmu[_reg.S++];
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void PHA()
    {
        _reg.S--;
        _mmu[_reg.S] = _reg.A;
    }

    void PLP()
    {
        _reg.P = _mmu[_reg.S++];
        // Set the unused bits to 1
        _reg.P |= 0x30;
    }

    void PHP()
    {
        _reg.S--;
        _mmu[_reg.S] = _reg.P;
        // Set the unused bits to 1
        _mmu[_reg.S] |= 0x30;
    }
}
