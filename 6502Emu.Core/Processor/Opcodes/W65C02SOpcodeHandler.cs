using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Utilities;

namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class W65C02SOpcodeHandler : Mos6502OpcodeHandler
{
    public W65C02SOpcodeHandler(Registers registers, Mmu mmu) : base(registers, mmu)
    {
    }

    void BBR(byte bit)
    {
        var value = ZeroPage();
        Branch(!value.IsBitSet(bit));
    }

    void BBS(byte bit)
    {
        var value = ZeroPage();
        Branch(value.IsBitSet(bit));
    }

    protected override void BRK()
    {
        base.BRK();
        _reg.ResetFlag(Flag.Decimal);
    }

    void DEC()
    {
        _reg.A--;
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void INC()
    {
        _reg.A++;
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void PHX()
    {
        _mmu[0x0100 + _reg.S] = _reg.X;
        _reg.S--;
    }

    void PHY()
    {
        _mmu[0x0100 + _reg.S] = _reg.Y;
        _reg.S--;
    }

    void PLX()
    {
        _reg.S++;
        _reg.X = _mmu[0x0100 + _reg.S];
        _reg.SetNegativeAndZeroFlags(_reg.X);
    }

    void PLY()
    {
        _reg.S++;
        _reg.Y = _mmu[0x0100 + _reg.S];
        _reg.SetNegativeAndZeroFlags(_reg.Y);
    }

    void RMB(byte bit)
    {
        var value = ZeroPage();
        byte mask = (byte)~(1 << bit);
        value &= mask;
        _mmu[_address] = value;
    }

    void SMB(byte bit)
    {
        var value = ZeroPage();
        byte mask = (byte)(1 << bit);
        value |= mask;
        _mmu[_address] = value;
    }

    void STZ(byte _)
    {
        _mmu[_address] = 0;
    }

    void TSB(byte value)
    {
        var andResult = (byte)(_reg.A & value); // for setting Z flag
        _reg.SetFlag(Flag.Zero, andResult == 0);

        var result = (byte)(_reg.A | value);    // the new value to write
        _mmu[_address] = result;
    }

    void TRB(byte value)
    {
        var andResult = (byte)(_reg.A & value); // for setting Z flag
        _reg.SetFlag(Flag.Zero, andResult == 0);

        var result = (byte)(~_reg.A & value);   // the new value to write
        _mmu[_address] = result;
    }

    void STP()
    {
        throw new NotImplementedException("STP not implemented");
    }

    void WAI()
    {
        throw new NotImplementedException("WAI not implemented");
    }

    protected override void ADC_Decimal(byte value, int carryIn)
    {
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

        var result = (byte)((highNibble << 4) | (lowNibble & 0x0F));

        _reg.SetNegativeAndZeroFlags(result);

        // Set the overflow flag if the sign of the result is different from the sign of the operands
        bool signedOverflow = (~(_reg.A ^ value) & (_reg.A ^ result) & 0x80) != 0;
        _reg.SetFlag(Flag.Overflow, signedOverflow);

        _reg.A = result;
    }

    protected override void SBC_Decimal(byte value, int carryIn)
    {
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
        var result = (byte)((highNibble << 4) | (lowNibble & 0x0F));

        _reg.SetNegativeAndZeroFlags(result);

        bool overflow = ((_reg.A ^ value) & (_reg.A ^ result) & 0x80) != 0;
        _reg.SetFlag(Flag.Overflow, overflow);


        _reg.A = result;
    }
}
