namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class Mos6502OpcodeHandler
{
    // https://en.wikibooks.org/wiki/6502_Assembly#Immediate:_#
    public byte Immediate() => _mmu[_reg.PC++];

    // https://en.wikibooks.org/wiki/6502_Assembly#Absolute:_a
    public byte Absolute()
    {
        _address = NextWord();
        return _mmu[_address];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Zero_Page:_zp
    public byte ZeroPage()
    {
        _address = NextByte();
        return _mmu[_address];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Relative:_r
    public byte Relative()
    {
        _address = (word)(_reg.PC + (sbyte)NextByte());
        return _mmu[_address];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Absolute_Indirect:_(a)
    public byte Indirect()
    {
        var indirect = NextWord();
        _lsb = _mmu[indirect];
        _msb = _mmu[(word)(indirect + 1)];
        _address = (word)(_msb << 8 | _lsb);
        return _mmu[_address];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Absolute_Indexed_with_X:_a,x
    public byte AbsoluteX()
    {
        _address = (word)(NextWord() + _reg.X);
        return _mmu[_address];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Absolute_Indexed_with_Y:_a,y
    public byte AbsoluteY()
    {
        _address = (word)(NextWord() + _reg.Y);
        return _mmu[_address];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Zero_Page_Indexed_with_X:_zp,x
    public byte ZeroPageX()
    {
        _address = (word)((NextByte() + (sbyte)_reg.X) & 0x00FF);
        return _mmu[_address];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Zero_Page_Indexed_with_Y:_zp,y
    public byte ZeroPageY()
    {
        _address = (word)((NextByte() + (sbyte)_reg.Y) & 0x00FF);
        return _mmu[_address];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Zero_Page_Indexed_Indirect:_(zp,x)
    public byte ZeroPageIndirectX()
    {
        var indirect = (word)((NextByte() + _reg.X) & 0x00FF);
        _lsb = _mmu[indirect];
        _msb = _mmu[(byte)(indirect + 1)];
        _address = (word)(_msb << 8 | _lsb);
        return _mmu[_address];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Zero_Page_Indirect_Indexed_with_Y:_(zp),y
    public byte ZeroPageIndirectY()
    {
        byte indirect = NextByte();
        _lsb = _mmu[indirect];
        _msb = _mmu[(byte)(indirect + 1)];
        _address = (word)((word)(_msb << 8 | _lsb) + _reg.Y);
        return _mmu[_address];
    }
}
