namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class Wd65C02OpcodeHandler
{
    protected override void InitializeMethods()
    {
        // Initialize the methods for the WDC 65C02
        base.InitializeMethods();

        _opcodes[0x80].Execute = () => Branch(true);   // BRA Relative

        _opcodes[0x12].Execute = () => ORA(Indirect());   // ORA (Indirect)
        _opcodes[0x32].Execute = () => AND(Indirect());   // AND (Indirect)
        _opcodes[0x52].Execute = () => EOR(Indirect());   // EOR (Indirect)
        _opcodes[0x72].Execute = () => ADC(Indirect());   // ADC (Indirect)
        _opcodes[0x92].Execute = () => STA(Indirect());   // STA (Indirect)
        _opcodes[0xB2].Execute = () => LDA(Indirect());   // LDA (Indirect)
        _opcodes[0xD2].Execute = () => CMP(Indirect());   // CMP (Indirect)
        _opcodes[0xF2].Execute = () => SBC(Indirect());   // SBC (Indirect)

        _opcodes[0x04].Execute = () => NOP();   // TSB Zero Page
        _opcodes[0x14].Execute = () => NOP();   // TRB Zero Page
        _opcodes[0x0C].Execute = () => NOP();   // TSB Absolute
        _opcodes[0x1C].Execute = () => NOP();   // TRB Absolute

        _opcodes[0x64].Execute = () => NOP();   // STZ Zero Page
        _opcodes[0x74].Execute = () => NOP();   // STZ Zero Page,X
        _opcodes[0x9C].Execute = () => NOP();   // STZ Absolute
        _opcodes[0x9E].Execute = () => NOP();   // STZ Absolute,X

        _opcodes[0x34].Execute = () => BIT(ZeroPageX());   // BIT Zero Page,X
        _opcodes[0x89].Execute = () => BIT(Immediate());   // BIT Immediate
        _opcodes[0x3C].Execute = () => BIT(AbsoluteX());   // BIT Absolute,X

        _opcodes[0x7C].Execute = () => NOP();   // JMP (Absolute,X)

        _opcodes[0x07].Execute = () => NOP();   // RMB0 Zero Page
        _opcodes[0x17].Execute = () => NOP();   // RMB1 Zero Page
        _opcodes[0x27].Execute = () => NOP();   // RMB2 Zero Page
        _opcodes[0x37].Execute = () => NOP();   // RMB3 Zero Page
        _opcodes[0x47].Execute = () => NOP();   // RMB4 Zero Page
        _opcodes[0x57].Execute = () => NOP();   // RMB5 Zero Page
        _opcodes[0x67].Execute = () => NOP();   // RMB6 Zero Page
        _opcodes[0x77].Execute = () => NOP();   // RMB7 Zero Page

        _opcodes[0x87].Execute = () => NOP();   // SMB0 Zero Page
        _opcodes[0x97].Execute = () => NOP();   // SMB1 Zero Page
        _opcodes[0xA7].Execute = () => NOP();   // SMB2 Zero Page
        _opcodes[0xB7].Execute = () => NOP();   // SMB3 Zero Page
        _opcodes[0xC7].Execute = () => NOP();   // SMB4 Zero Page
        _opcodes[0xD7].Execute = () => NOP();   // SMB5 Zero Page
        _opcodes[0xE7].Execute = () => NOP();   // SMB6 Zero Page
        _opcodes[0xF7].Execute = () => NOP();   // SMB7 Zero Page

        _opcodes[0x1A].Execute = () => INC();   // INC Implied
        _opcodes[0x3A].Execute = () => DEC();   // DEC Implied
        _opcodes[0x5A].Execute = () => PHY();   // PHY Implied
        _opcodes[0x7A].Execute = () => PLY();   // PLY Implied
        _opcodes[0xDA].Execute = () => PHX();   // PHX Implied
        _opcodes[0xFA].Execute = () => PLX();   // PLX Implied
        _opcodes[0xCB].Execute = () => WAI();   // WAI Implied
        _opcodes[0xDB].Execute = () => STP();   // STP Implied

        _opcodes[0x0F].Execute = () => NOP();   // BBR0 Relative
        _opcodes[0x1F].Execute = () => NOP();   // BBR1 Relative
        _opcodes[0x2F].Execute = () => NOP();   // BBR2 Relative
        _opcodes[0x3F].Execute = () => NOP();   // BBR3 Relative
        _opcodes[0x4F].Execute = () => NOP();   // BBR4 Relative
        _opcodes[0x5F].Execute = () => NOP();   // BBR5 Relative
        _opcodes[0x6F].Execute = () => NOP();   // BBR6 Relative
        _opcodes[0x7F].Execute = () => NOP();   // BBR7 Relative

        _opcodes[0x8F].Execute = () => NOP();   // BBS0 Relative
        _opcodes[0x9F].Execute = () => NOP();   // BBS1 Relative
        _opcodes[0xAF].Execute = () => NOP();   // BBS2 Relative
        _opcodes[0xBF].Execute = () => NOP();   // BBS3 Relative
        _opcodes[0xCF].Execute = () => NOP();   // BBS4 Relative
        _opcodes[0xDF].Execute = () => NOP();   // BBS5 Relative
        _opcodes[0xEF].Execute = () => NOP();   // BBS6 Relative
        _opcodes[0xFF].Execute = () => NOP();   // BBS7 Relative
    }
}
