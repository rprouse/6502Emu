using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Utilities;

namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class Wd65C02OpcodeHandler : Mos6502OpcodeHandler
{
    public Wd65C02OpcodeHandler(Registers registers, Mmu mmu) : base(registers, mmu)
    {
    }

    void INC()
    {
        _reg.A++;
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void DEC()
    {
        _reg.A--;
        _reg.SetNegativeAndZeroFlags(_reg.A);
    }

    void PLX()
    {
        _reg.S++;
        _reg.X = _mmu[0x0100 + _reg.S];
        _reg.SetNegativeAndZeroFlags(_reg.X);
    }

    void PHX()
    {
        _mmu[0x0100 + _reg.S] = _reg.X;
        _reg.S--;
    }

    void PLY()
    {
        _reg.S++;
        _reg.Y = _mmu[0x0100 + _reg.S];
        _reg.SetNegativeAndZeroFlags(_reg.Y);
    }

    void PHY()
    {
        _mmu[0x0100 + _reg.S] = _reg.Y;
        _reg.S--;
    }

    void WAI()
    {
        throw new NotImplementedException("WAI not implemented");
    }

    void STP()
    {
        throw new NotImplementedException("STP not implemented");
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

    void TSB(byte value)
    {
        var result = (byte)(_reg.A | value);
        _mmu[_address] = result;
        _reg.SetFlag(Flag.Zero, result == 0);
    }

    void TRB(byte value)
    {
        var result = (byte)(~_reg.A & value);
        _mmu[_address] = result;
        _reg.SetFlag(Flag.Zero, result == 0);
    }

    void STZ(byte _)
    {        
        _mmu[_address] = 0;
    }
}
