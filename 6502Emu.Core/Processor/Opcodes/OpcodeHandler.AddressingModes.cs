namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class OpcodeHandler
{
    // https://en.wikibooks.org/wiki/6502_Assembly#Immediate:_#
    public byte Immediate() => _mmu[_reg.PC++];

    // https://en.wikibooks.org/wiki/6502_Assembly#Absolute:_a
    public byte Absolute() => _mmu[NextWord()];

    // https://en.wikibooks.org/wiki/6502_Assembly#Zero_Page:_zp
    public byte ZeroPage() => _mmu[NextByte()];

    // https://en.wikibooks.org/wiki/6502_Assembly#Relative:_r
    public byte Relative()
    {
        _address = (word)(_reg.PC + (sbyte)NextByte());
        return _mmu[_address];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Absolute_Indirect:_(a)
    public byte AbsoluteIndirect()
    {
        _address = NextWord();
        _lsb = _mmu[_address];
        _msb = _mmu[(word)(_address + 1)];
        return _mmu[(_msb << 8) | _lsb];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Absolute_Indexed_with_X:_a,x
    public byte AbsoluteX()
    {
        _address = NextWord();
        return _mmu[_address + _reg.X];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Absolute_Indexed_with_Y:_a,y
    public byte AbsoluteY()
    {
        _address = NextWord();
        return _mmu[_address + _reg.Y];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Zero_Page_Indexed_with_X:_zp,x
    public byte ZeroPageX() => _mmu[NextByte() + _reg.X];

    // https://en.wikibooks.org/wiki/6502_Assembly#Zero_Page_Indexed_with_Y:_zp,y
    public byte ZeroPageY() => _mmu[NextByte() + _reg.Y];

    // https://en.wikibooks.org/wiki/6502_Assembly#Zero_Page_Indexed_Indirect:_(zp,x)
    public byte ZeroPageIndirectX()
    {
        _address = (word)(NextByte() + _reg.X);
        _lsb = _mmu[_address];
        _msb = _mmu[(word)(_address + 1)];
        return _mmu[(_msb << 8) | _lsb];
    }

    // https://en.wikibooks.org/wiki/6502_Assembly#Zero_Page_Indirect_Indexed_with_Y:_(zp),y
    public byte ZeroPageIndirectY()
    {
        _address = NextByte();
        _lsb = _mmu[_address];
        _msb = _mmu[(word)(_address + 1)];
        return _mmu[(_msb << 8) | _lsb + _reg.Y];
    }
}
