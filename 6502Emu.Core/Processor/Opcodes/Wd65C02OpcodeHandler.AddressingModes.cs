namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class Wd65C02OpcodeHandler
{
    public byte ZeroPageIndirect()
    {
        var indirect = (word)(NextByte() & 0x00FF);
        _lsb = _mmu[indirect];
        _msb = _mmu[(byte)(indirect + 1)];
        _address = (word)(_msb << 8 | _lsb);
        return _mmu[_address];
    }

    public byte AbsoluteIndexedIndirect()
    {
        var indirect = (word)(NextWord() + _reg.X);
        _lsb = _mmu[indirect];
        _msb = _mmu[indirect + 1];
        _address = (word)(_msb << 8 | _lsb);
        return _mmu[_address];
    }
}
