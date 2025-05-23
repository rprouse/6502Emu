namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class W65C02SOpcodeHandler
{
    protected override void InitializeMethods()
    {
        // Initialize the methods for the WDC 65C02
        base.InitializeMethods();

        _opcodes[0x80].Execute = () => Branch(true);   // BRA Relative

        _opcodes[0x12].Execute = () => ORA(ZeroPageIndirect());   // ORA (Zero Page)
        _opcodes[0x32].Execute = () => AND(ZeroPageIndirect());   // AND (Zero Page)
        _opcodes[0x52].Execute = () => EOR(ZeroPageIndirect());   // EOR (Zero Page)
        _opcodes[0x72].Execute = () => ADC(ZeroPageIndirect());   // ADC (Zero Page)
        _opcodes[0x92].Execute = () => STA(ZeroPageIndirect());   // STA (Zero Page)
        _opcodes[0xB2].Execute = () => LDA(ZeroPageIndirect());   // LDA (Zero Page)
        _opcodes[0xD2].Execute = () => CMP(ZeroPageIndirect());   // CMP (Zero Page)
        _opcodes[0xF2].Execute = () => SBC(ZeroPageIndirect());   // SBC (Zero Page)

        _opcodes[0x04].Execute = () => TSB(ZeroPage());   // TSB Zero Page
        _opcodes[0x14].Execute = () => TRB(ZeroPage());   // TRB Zero Page
        _opcodes[0x0C].Execute = () => TSB(Absolute());   // TSB Absolute
        _opcodes[0x1C].Execute = () => TRB(Absolute());   // TRB Absolute

        _opcodes[0x64].Execute = () => STZ(ZeroPage());   // STZ Zero Page
        _opcodes[0x74].Execute = () => STZ(ZeroPageX());   // STZ Zero Page,X
        _opcodes[0x9C].Execute = () => STZ(Absolute());   // STZ Absolute
        _opcodes[0x9E].Execute = () => STZ(AbsoluteX());   // STZ Absolute,X

        _opcodes[0x34].Execute = () => BIT(ZeroPageX());   // BIT Zero Page,X
        _opcodes[0x89].Execute = () => BIT(Immediate());   // BIT Immediate
        _opcodes[0x3C].Execute = () => BIT(AbsoluteX());   // BIT Absolute,X

        _opcodes[0x7C].Execute = () => JMP(AbsoluteIndexedIndirect());   // JMP (Absolute,X)

        _opcodes[0x07].Execute = () => RMB(0);   // RMB0 Zero Page
        _opcodes[0x17].Execute = () => RMB(1);   // RMB1 Zero Page
        _opcodes[0x27].Execute = () => RMB(2);   // RMB2 Zero Page
        _opcodes[0x37].Execute = () => RMB(3);   // RMB3 Zero Page
        _opcodes[0x47].Execute = () => RMB(4);   // RMB4 Zero Page
        _opcodes[0x57].Execute = () => RMB(5);   // RMB5 Zero Page
        _opcodes[0x67].Execute = () => RMB(6);   // RMB6 Zero Page
        _opcodes[0x77].Execute = () => RMB(7);   // RMB7 Zero Page

        _opcodes[0x87].Execute = () => SMB(0);   // SMB0 Zero Page
        _opcodes[0x97].Execute = () => SMB(1);   // SMB1 Zero Page
        _opcodes[0xA7].Execute = () => SMB(2);   // SMB2 Zero Page
        _opcodes[0xB7].Execute = () => SMB(3);   // SMB3 Zero Page
        _opcodes[0xC7].Execute = () => SMB(4);   // SMB4 Zero Page
        _opcodes[0xD7].Execute = () => SMB(5);   // SMB5 Zero Page
        _opcodes[0xE7].Execute = () => SMB(6);   // SMB6 Zero Page
        _opcodes[0xF7].Execute = () => SMB(7);   // SMB7 Zero Page

        _opcodes[0x1A].Execute = () => INC();   // INC Implied
        _opcodes[0x3A].Execute = () => DEC();   // DEC Implied
        _opcodes[0x5A].Execute = () => PHY();   // PHY Implied
        _opcodes[0x7A].Execute = () => PLY();   // PLY Implied
        _opcodes[0xDA].Execute = () => PHX();   // PHX Implied
        _opcodes[0xFA].Execute = () => PLX();   // PLX Implied
        _opcodes[0xCB].Execute = () => WAI();   // WAI Implied
        _opcodes[0xDB].Execute = () => STP();   // STP Implied

        _opcodes[0x0F].Execute = () => BBR(0);   // BBR0 Relative
        _opcodes[0x1F].Execute = () => BBR(1);   // BBR1 Relative
        _opcodes[0x2F].Execute = () => BBR(2);   // BBR2 Relative
        _opcodes[0x3F].Execute = () => BBR(3);   // BBR3 Relative
        _opcodes[0x4F].Execute = () => BBR(4);   // BBR4 Relative
        _opcodes[0x5F].Execute = () => BBR(5);   // BBR5 Relative
        _opcodes[0x6F].Execute = () => BBR(6);   // BBR6 Relative
        _opcodes[0x7F].Execute = () => BBR(7);   // BBR7 Relative

        _opcodes[0x8F].Execute = () => BBS(0);   // BBS0 Relative
        _opcodes[0x9F].Execute = () => BBS(1);   // BBS1 Relative
        _opcodes[0xAF].Execute = () => BBS(2);   // BBS2 Relative
        _opcodes[0xBF].Execute = () => BBS(3);   // BBS3 Relative
        _opcodes[0xCF].Execute = () => BBS(4);   // BBS4 Relative
        _opcodes[0xDF].Execute = () => BBS(5);   // BBS5 Relative
        _opcodes[0xEF].Execute = () => BBS(6);   // BBS6 Relative
        _opcodes[0xFF].Execute = () => BBS(7);   // BBS7 Relative
    }
}
