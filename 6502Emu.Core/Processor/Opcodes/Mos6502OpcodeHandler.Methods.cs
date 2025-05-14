namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class Mos6502OpcodeHandler
{
    private void InitializeMethods()
    {
        // Add with Carry
        _opcodes[0x69].Execute = () => ADC(Immediate());  // ADC Immediate
        _opcodes[0x65].Execute = () => ADC(ZeroPage());  // ADC Zero Page
        _opcodes[0x75].Execute = () => ADC(ZeroPageX());  // ADC Zero Page,X
        _opcodes[0x6D].Execute = () => ADC(Absolute());  // ADC Absolute
        _opcodes[0x7D].Execute = () => ADC(AbsoluteX());  // ADC Absolute,X
        _opcodes[0x79].Execute = () => ADC(AbsoluteY());  // ADC Absolute,Y
        _opcodes[0x61].Execute = () => ADC(ZeroPageIndirectX());  // ADC (Indirect,X)
        _opcodes[0x71].Execute = () => ADC(ZeroPageIndirectY());  // ADC (Indirect),Y

        // Logical AND
        _opcodes[0x29].Execute = () => AND(Immediate());  // AND Immediate
        _opcodes[0x25].Execute = () => AND(ZeroPage());  // AND Zero Page
        _opcodes[0x35].Execute = () => AND(ZeroPageX());  // AND Zero Page,X
        _opcodes[0x2D].Execute = () => AND(Absolute());  // AND Absolute
        _opcodes[0x3D].Execute = () => AND(AbsoluteX());  // AND Absolute,X
        _opcodes[0x39].Execute = () => AND(AbsoluteY());  // AND Absolute,Y
        _opcodes[0x21].Execute = () => AND(ZeroPageIndirectX());  // AND (Indirect,X)
        _opcodes[0x31].Execute = () => AND(ZeroPageIndirectY());  // AND (Indirect),Y

        // Exclusive OR
        _opcodes[0x49].Execute = () => EOR(Immediate());  // EOR Immediate
        _opcodes[0x45].Execute = () => EOR(ZeroPage());  // EOR Zero Page
        _opcodes[0x55].Execute = () => EOR(ZeroPageX());  // EOR Zero Page,X
        _opcodes[0x4D].Execute = () => EOR(Absolute());  // EOR Absolute
        _opcodes[0x5D].Execute = () => EOR(AbsoluteX());  // EOR Absolute,X
        _opcodes[0x59].Execute = () => EOR(AbsoluteY());  // EOR Absolute,Y
        _opcodes[0x41].Execute = () => EOR(ZeroPageIndirectX());  // EOR (Indirect,X)
        _opcodes[0x51].Execute = () => EOR(ZeroPageIndirectY());  // EOR (Indirect),Y

        // Logical OR (ORA)
        _opcodes[0x09].Execute = () => ORA(Immediate());  // ORA Immediate
        _opcodes[0x05].Execute = () => ORA(ZeroPage());  // ORA Zero Page
        _opcodes[0x15].Execute = () => ORA(ZeroPageX());  // ORA Zero Page,X
        _opcodes[0x0D].Execute = () => ORA(Absolute());  // ORA Absolute
        _opcodes[0x1D].Execute = () => ORA(AbsoluteX());  // ORA Absolute,X
        _opcodes[0x19].Execute = () => ORA(AbsoluteY());  // ORA Absolute,Y
        _opcodes[0x01].Execute = () => ORA(ZeroPageIndirectX());  // ORA (Indirect,X)
        _opcodes[0x11].Execute = () => ORA(ZeroPageIndirectY());  // ORA (Indirect),Y

        // Arithmetic Shift Left
        _opcodes[0x0A].Execute = () => ASL();  // ASL Accumulator
        _opcodes[0x06].Execute = () => ASL(ZeroPage());  // ASL Zero Page
        _opcodes[0x16].Execute = () => ASL(ZeroPageX());  // ASL Zero Page,X
        _opcodes[0x0E].Execute = () => ASL(Absolute());  // ASL Absolute
        _opcodes[0x1E].Execute = () => ASL(AbsoluteX());  // ASL Absolute,X

        // Logical Shift Right
        _opcodes[0x4A].Execute = () => LSR();  // LSR Accumulator
        _opcodes[0x46].Execute = () => LSR(ZeroPage());  // LSR Zero Page
        _opcodes[0x56].Execute = () => LSR(ZeroPageX());  // LSR Zero Page,X
        _opcodes[0x4E].Execute = () => LSR(Absolute());  // LSR Absolute
        _opcodes[0x5E].Execute = () => LSR(AbsoluteX());  // LSR Absolute,X

        // Rotate Left
        _opcodes[0x2A].Execute = () => ROL();  // ROL Accumulator
        _opcodes[0x26].Execute = () => ROL(ZeroPage());  // ROL Zero Page
        _opcodes[0x36].Execute = () => ROL(ZeroPageX());  // ROL Zero Page,X
        _opcodes[0x2E].Execute = () => ROL(Absolute());  // ROL Absolute
        _opcodes[0x3E].Execute = () => ROL(AbsoluteX());  // ROL Absolute,X

        // Rotate Right
        _opcodes[0x6A].Execute = () => ROR();  // ROR Accumulator
        _opcodes[0x66].Execute = () => ROR(ZeroPage());  // ROR Zero Page
        _opcodes[0x76].Execute = () => ROR(ZeroPageX());  // ROR Zero Page,X
        _opcodes[0x6E].Execute = () => ROR(Absolute());  // ROR Absolute
        _opcodes[0x7E].Execute = () => ROR(AbsoluteX());  // ROR Absolute,X

        // Decrement Memory
        _opcodes[0xC6].Execute = () => DEC(ZeroPage());  // DEC Zero Page
        _opcodes[0xD6].Execute = () => DEC(ZeroPageX());  // DEC Zero Page,X
        _opcodes[0xCE].Execute = () => DEC(Absolute());  // DEC Absolute
        _opcodes[0xDE].Execute = () => DEC(AbsoluteX());  // DEC Absolute,X

        // Increment Memory
        _opcodes[0xE6].Execute = () => INC(ZeroPage());  // INC Zero Page
        _opcodes[0xF6].Execute = () => INC(ZeroPageX());  // INC Zero Page,X
        _opcodes[0xEE].Execute = () => INC(Absolute());  // INC Absolute
        _opcodes[0xFE].Execute = () => INC(AbsoluteX());  // INC Absolute,X

        // Increment/Decrement Register Instructions
        _opcodes[0xE8].Execute = () => INX();  // INX Implied
        _opcodes[0xCA].Execute = () => DEX();  // DEX Implied
        _opcodes[0xC8].Execute = () => INY();  // INY Implied
        _opcodes[0x88].Execute = () => DEY();  // DEY Implied

        // No Operation
        _opcodes[0xEA].Execute = () => NOP();  // NOP Implied

        // Branch Instructions
        _opcodes[0x90].Execute = () => Branch(!_reg.GetFlag(Flag.Carry));  // BCC
        _opcodes[0xB0].Execute = () => Branch(_reg.GetFlag(Flag.Carry));  // BCS
        _opcodes[0xD0].Execute = () => Branch(!_reg.GetFlag(Flag.Zero));  // BNE
        _opcodes[0xF0].Execute = () => Branch(_reg.GetFlag(Flag.Zero));  // BEQ
        _opcodes[0x10].Execute = () => Branch(!_reg.GetFlag(Flag.Negative));  // BPL
        _opcodes[0x30].Execute = () => Branch(_reg.GetFlag(Flag.Negative));  // BMI
        _opcodes[0x50].Execute = () => Branch(!_reg.GetFlag(Flag.Overflow));  // BVC
        _opcodes[0x70].Execute = () => Branch(_reg.GetFlag(Flag.Overflow));  // BVS

        // Bit Test
        _opcodes[0x24].Execute = () => BIT(ZeroPage());  // BIT Zero Page
        _opcodes[0x2C].Execute = () => BIT(Absolute());  // BIT Absolute

        // Break
        _opcodes[0x00].Execute = () => BRK();  // BRK

        // Clear Flags
        _opcodes[0x18].Execute = () => _reg.ResetFlag(Flag.Carry);  // CLC
        _opcodes[0xD8].Execute = () => _reg.ResetFlag(Flag.Decimal);  // CLD
        _opcodes[0x58].Execute = () => _reg.ResetFlag(Flag.Interupt);  // CLI
        _opcodes[0xB8].Execute = () => _reg.ResetFlag(Flag.Overflow);  // CLV

        // Set Flags
        _opcodes[0x38].Execute = () => _reg.SetFlag(Flag.Carry);  // SEC
        _opcodes[0xF8].Execute = () => _reg.SetFlag(Flag.Decimal);  // SED
        _opcodes[0x78].Execute = () => _reg.SetFlag(Flag.Interupt);  // SEI

        // Jump and Call Instructions
        _opcodes[0x20].Execute = () => JSR(Absolute());  // JSR Absolute
        _opcodes[0x60].Execute = () => RTS();  // RTS Implied
        _opcodes[0x40].Execute = () => RTI();  // RTI Implied
        _opcodes[0x4C].Execute = () => JMP(Absolute());  // JMP Absolute
        _opcodes[0x6C].Execute = () => JMP(Indirect());  // JMP Indirect

        // Compare
        _opcodes[0xC9].Execute = () => CMP(Immediate());  // CMP Immediate
        _opcodes[0xC5].Execute = () => CMP(ZeroPage());  // CMP Zero Page
        _opcodes[0xD5].Execute = () => CMP(ZeroPageX());  // CMP Zero Page,X
        _opcodes[0xCD].Execute = () => CMP(Absolute());  // CMP Absolute
        _opcodes[0xDD].Execute = () => CMP(AbsoluteX());  // CMP Absolute,X
        _opcodes[0xD9].Execute = () => CMP(AbsoluteY());  // CMP Absolute,Y
        _opcodes[0xC1].Execute = () => CMP(ZeroPageIndirectX());  // CMP (Indirect,X)
        _opcodes[0xD1].Execute = () => CMP(ZeroPageIndirectY());  // CMP (Indirect),Y

        // Compare X Register
        _opcodes[0xE0].Execute = () => CPX(Immediate());  // CPX Immediate
        _opcodes[0xE4].Execute = () => CPX(ZeroPage());  // CPX Zero Page
        _opcodes[0xEC].Execute = () => CPX(Absolute());  // CPX Absolute

        // Compare Y Register
        _opcodes[0xC0].Execute = () => CPY(Immediate());  // CPY Immediate
        _opcodes[0xC4].Execute = () => CPY(ZeroPage());  // CPY Zero Page
        _opcodes[0xCC].Execute = () => CPY(Absolute());  // CPY Absolute

        // Load Accumulator
        _opcodes[0xA9].Execute = () => LDA(Immediate());  // LDA Immediate
        _opcodes[0xA5].Execute = () => LDA(ZeroPage());  // LDA Zero Page
        _opcodes[0xB5].Execute = () => LDA(ZeroPageX());  // LDA Zero Page,X
        _opcodes[0xAD].Execute = () => LDA(Absolute());  // LDA Absolute
        _opcodes[0xBD].Execute = () => LDA(AbsoluteX());  // LDA Absolute,X
        _opcodes[0xB9].Execute = () => LDA(AbsoluteY());  // LDA Absolute,Y
        _opcodes[0xA1].Execute = () => LDA(ZeroPageIndirectX());  // LDA (Indirect,X)
        _opcodes[0xB1].Execute = () => LDA(ZeroPageIndirectY());  // LDA (Indirect),Y

        // Load X Register
        _opcodes[0xA2].Execute = () => LDX(Immediate());  // LDX Immediate
        _opcodes[0xA6].Execute = () => LDX(ZeroPage());  // LDX Zero Page
        _opcodes[0xB6].Execute = () => LDX(ZeroPageY());  // LDX Zero Page,Y
        _opcodes[0xAE].Execute = () => LDX(Absolute());  // LDX Absolute
        _opcodes[0xBE].Execute = () => LDX(AbsoluteY());  // LDX Absolute,Y

        // Load Y Register
        _opcodes[0xA0].Execute = () => LDY(Immediate());  // LDY Immediate
        _opcodes[0xA4].Execute = () => LDY(ZeroPage());  // LDY Zero Page
        _opcodes[0xB4].Execute = () => LDY(ZeroPageX());  // LDY Zero Page,X
        _opcodes[0xAC].Execute = () => LDY(Absolute());  // LDY Absolute
        _opcodes[0xBC].Execute = () => LDY(AbsoluteX());  // LDY Absolute,X

        // Transfer Instructions
        _opcodes[0xBA].Execute = () => TSX();  // TSX Implied
        _opcodes[0x8A].Execute = () => TXA();  // TXA Implied
        _opcodes[0x98].Execute = () => TYA();  // TYA Implied
        _opcodes[0xAA].Execute = () => TAX();  // TAX Implied
        _opcodes[0xA8].Execute = () => TAY();  // TAY Implied
        _opcodes[0x9A].Execute = () => TXS();  // TXS Implied

        // Stack Operations
        _opcodes[0x68].Execute = () => PLA();  // PLA Implied
        _opcodes[0x48].Execute = () => PHA();  // PHA Implied
        _opcodes[0x28].Execute = () => PLP();  // PLP Implied
        _opcodes[0x08].Execute = () => PHP();  // PHP Implied

        // Subtract with Carry
        _opcodes[0xE9].Execute = () => SBC(Immediate());  // SBC Immediate
        _opcodes[0xE5].Execute = () => SBC(ZeroPage());  // SBC Zero Page
        _opcodes[0xF5].Execute = () => SBC(ZeroPageX());  // SBC Zero Page,X
        _opcodes[0xED].Execute = () => SBC(Absolute());  // SBC Absolute
        _opcodes[0xFD].Execute = () => SBC(AbsoluteX());  // SBC Absolute,X
        _opcodes[0xF9].Execute = () => SBC(AbsoluteY());  // SBC Absolute,Y
        _opcodes[0xE1].Execute = () => SBC(ZeroPageIndirectX());  // SBC (Indirect,X)
        _opcodes[0xF1].Execute = () => SBC(ZeroPageIndirectY());  // SBC (Indirect),Y

        // Store Accumulator
        _opcodes[0x85].Execute = () => STA(ZeroPage());  // STA Zero Page
        _opcodes[0x95].Execute = () => STA(ZeroPageX());  // STA Zero Page,X
        _opcodes[0x8D].Execute = () => STA(Absolute());  // STA Absolute
        _opcodes[0x9D].Execute = () => STA(AbsoluteX());  // STA Absolute,X
        _opcodes[0x99].Execute = () => STA(AbsoluteY());  // STA Absolute,Y
        _opcodes[0x81].Execute = () => STA(ZeroPageIndirectX());  // STA (Indirect,X)
        _opcodes[0x91].Execute = () => STA(ZeroPageIndirectY());  // STA (Indirect),Y

        // Store X Register
        _opcodes[0x86].Execute = () => STX(ZeroPage());  // STX Zero Page
        _opcodes[0x96].Execute = () => STX(ZeroPageY());  // STX Zero Page,Y
        _opcodes[0x8E].Execute = () => STX(Absolute());  // STX Absolute

        // Store Y Register
        _opcodes[0x84].Execute = () => STY(ZeroPage());  // STY Zero Page
        _opcodes[0x94].Execute = () => STY(ZeroPageX());  // STY Zero Page,X
        _opcodes[0x8C].Execute = () => STY(Absolute());  // STY Absolute
    }
}
