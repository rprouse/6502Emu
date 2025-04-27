namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class OpcodeHandler
{
    private void InitializeMethods()
    {
        // Add with Carry
        _opcodes[0x69].Execute = () => NOP();  // ADC Immediate
        _opcodes[0x65].Execute = () => NOP();  // ADC Zero Page
        _opcodes[0x75].Execute = () => NOP();  // ADC Zero Page,X
        _opcodes[0x6D].Execute = () => NOP();  // ADC Absolute
        _opcodes[0x7D].Execute = () => NOP();  // ADC Absolute,X
        _opcodes[0x79].Execute = () => NOP();  // ADC Absolute,Y
        _opcodes[0x61].Execute = () => NOP();  // ADC (Indirect,X)
        _opcodes[0x71].Execute = () => NOP();  // ADC (Indirect),Y

        // Logical AND
        _opcodes[0x29].Execute = () => NOP();  // AND Immediate
        _opcodes[0x25].Execute = () => NOP();  // AND Zero Page
        _opcodes[0x35].Execute = () => NOP();  // AND Zero Page,X
        _opcodes[0x2D].Execute = () => NOP();  // AND Absolute
        _opcodes[0x3D].Execute = () => NOP();  // AND Absolute,X
        _opcodes[0x39].Execute = () => NOP();  // AND Absolute,Y
        _opcodes[0x21].Execute = () => NOP();  // AND (Indirect,X)
        _opcodes[0x31].Execute = () => NOP();  // AND (Indirect),Y

        // Exclusive OR
        _opcodes[0x49].Execute = () => NOP();  // EOR Immediate
        _opcodes[0x45].Execute = () => NOP();  // EOR Zero Page
        _opcodes[0x55].Execute = () => NOP();  // EOR Zero Page,X
        _opcodes[0x4D].Execute = () => NOP();  // EOR Absolute
        _opcodes[0x5D].Execute = () => NOP();  // EOR Absolute,X
        _opcodes[0x59].Execute = () => NOP();  // EOR Absolute,Y
        _opcodes[0x41].Execute = () => NOP();  // EOR (Indirect,X)
        _opcodes[0x51].Execute = () => NOP();  // EOR (Indirect),Y

        // Logical OR (ORA)
        _opcodes[0x09].Execute = () => NOP();  // ORA Immediate
        _opcodes[0x05].Execute = () => NOP();  // ORA Zero Page
        _opcodes[0x15].Execute = () => NOP();  // ORA Zero Page,X
        _opcodes[0x0D].Execute = () => NOP();  // ORA Absolute
        _opcodes[0x1D].Execute = () => NOP();  // ORA Absolute,X
        _opcodes[0x19].Execute = () => NOP();  // ORA Absolute,Y
        _opcodes[0x01].Execute = () => NOP();  // ORA (Indirect,X)
        _opcodes[0x11].Execute = () => NOP();  // ORA (Indirect),Y

        // Arithmetic Shift Left
        _opcodes[0x0A].Execute = () => NOP();  // ASL Accumulator
        _opcodes[0x06].Execute = () => NOP();  // ASL Zero Page
        _opcodes[0x16].Execute = () => NOP();  // ASL Zero Page,X
        _opcodes[0x0E].Execute = () => NOP();  // ASL Absolute
        _opcodes[0x1E].Execute = () => NOP();  // ASL Absolute,X

        // Logical Shift Right
        _opcodes[0x4A].Execute = () => NOP();  // LSR Accumulator
        _opcodes[0x46].Execute = () => NOP();  // LSR Zero Page
        _opcodes[0x56].Execute = () => NOP();  // LSR Zero Page,X
        _opcodes[0x4E].Execute = () => NOP();  // LSR Absolute
        _opcodes[0x5E].Execute = () => NOP();  // LSR Absolute,X

        // Rotate Left
        _opcodes[0x2A].Execute = () => NOP();  // ROL Accumulator
        _opcodes[0x26].Execute = () => NOP();  // ROL Zero Page
        _opcodes[0x36].Execute = () => NOP();  // ROL Zero Page,X
        _opcodes[0x2E].Execute = () => NOP();  // ROL Absolute
        _opcodes[0x3E].Execute = () => NOP();  // ROL Absolute,X

        // Rotate Right
        _opcodes[0x6A].Execute = () => NOP();  // ROR Accumulator
        _opcodes[0x66].Execute = () => NOP();  // ROR Zero Page
        _opcodes[0x76].Execute = () => NOP();  // ROR Zero Page,X
        _opcodes[0x6E].Execute = () => NOP();  // ROR Absolute
        _opcodes[0x7E].Execute = () => NOP();  // ROR Absolute,X

        // Decrement Memory
        _opcodes[0xC6].Execute = () => NOP();  // DEC Zero Page
        _opcodes[0xD6].Execute = () => NOP();  // DEC Zero Page,X
        _opcodes[0xCE].Execute = () => NOP();  // DEC Absolute
        _opcodes[0xDE].Execute = () => NOP();  // DEC Absolute,X

        // Increment Memory
        _opcodes[0xE6].Execute = () => NOP();  // INC Zero Page
        _opcodes[0xF6].Execute = () => NOP();  // INC Zero Page,X
        _opcodes[0xEE].Execute = () => NOP();  // INC Absolute
        _opcodes[0xFE].Execute = () => NOP();  // INC Absolute,X

        // Increment/Decrement Register Instructions
        _opcodes[0xE8].Execute = () => NOP();  // INX Implied
        _opcodes[0xCA].Execute = () => NOP();  // DEX Implied
        _opcodes[0xC8].Execute = () => NOP();  // INY Implied
        _opcodes[0x88].Execute = () => NOP();  // DEY Implied

        // No Operation
        _opcodes[0xEA].Execute = () => NOP();  // NOP Implied

        // Branch Instructions
        _opcodes[0x90].Execute = () => NOP();  // BCC
        _opcodes[0xB0].Execute = () => NOP();  // BCS
        _opcodes[0xF0].Execute = () => NOP();  // BEQ
        _opcodes[0x30].Execute = () => NOP();  // BMI
        _opcodes[0xD0].Execute = () => NOP();  // BNE
        _opcodes[0x10].Execute = () => NOP();  // BPL
        _opcodes[0x50].Execute = () => NOP();  // BVC
        _opcodes[0x70].Execute = () => NOP();  // BVS

        // Bit Test
        _opcodes[0x24].Execute = () => NOP();  // BIT Zero Page
        _opcodes[0x2C].Execute = () => NOP();  // BIT Absolute

        // Break
        _opcodes[0x00].Execute = () => NOP();  // BRK

        // Clear Flags
        _opcodes[0x18].Execute = () => NOP();  // CLC
        _opcodes[0xD8].Execute = () => NOP();  // CLD
        _opcodes[0x58].Execute = () => NOP();  // CLI
        _opcodes[0xB8].Execute = () => NOP();  // CLV

        // Set Flags
        _opcodes[0x38].Execute = () => NOP();  // SEC
        _opcodes[0xF8].Execute = () => NOP();  // SED
        _opcodes[0x78].Execute = () => NOP();  // SEI

        // Jump and Call Instructions
        _opcodes[0x20].Execute = () => NOP();  // JSR Absolute
        _opcodes[0x60].Execute = () => NOP();  // RTS Implied
        _opcodes[0x40].Execute = () => NOP();  // RTI Implied
        _opcodes[0x4C].Execute = () => NOP();  // JMP Absolute
        _opcodes[0x6C].Execute = () => NOP();  // JMP Indirect

        // Compare
        _opcodes[0xC9].Execute = () => NOP();  // CMP Immediate
        _opcodes[0xC5].Execute = () => NOP();  // CMP Zero Page
        _opcodes[0xD5].Execute = () => NOP();  // CMP Zero Page,X
        _opcodes[0xCD].Execute = () => NOP();  // CMP Absolute
        _opcodes[0xDD].Execute = () => NOP();  // CMP Absolute,X
        _opcodes[0xD9].Execute = () => NOP();  // CMP Absolute,Y
        _opcodes[0xC1].Execute = () => NOP();  // CMP (Indirect,X)
        _opcodes[0xD1].Execute = () => NOP();  // CMP (Indirect),Y

        // Compare X Register
        _opcodes[0xE0].Execute = () => NOP();  // CPX Immediate
        _opcodes[0xE4].Execute = () => NOP();  // CPX Zero Page
        _opcodes[0xEC].Execute = () => NOP();  // CPX Absolute

        // Compare Y Register
        _opcodes[0xC0].Execute = () => NOP();  // CPY Immediate
        _opcodes[0xC4].Execute = () => NOP();  // CPY Zero Page
        _opcodes[0xCC].Execute = () => NOP();  // CPY Absolute

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
        _opcodes[0xBA].Execute = () => NOP();  // TSX Implied
        _opcodes[0x8A].Execute = () => NOP();  // TXA Implied
        _opcodes[0x98].Execute = () => NOP();  // TYA Implied
        _opcodes[0xAA].Execute = () => NOP();  // TAX Implied
        _opcodes[0xA8].Execute = () => NOP();  // TAY Implied
        _opcodes[0x9A].Execute = () => NOP();  // TXS Implied

        // Stack Operations
        _opcodes[0x68].Execute = () => NOP();  // PLA Implied
        _opcodes[0x48].Execute = () => NOP();  // PHA Implied
        _opcodes[0x28].Execute = () => NOP();  // PLP Implied
        _opcodes[0x08].Execute = () => NOP();  // PHP Implied

        // Subtract with Carry
        _opcodes[0xE9].Execute = () => NOP();  // SBC Immediate
        _opcodes[0xE5].Execute = () => NOP();  // SBC Zero Page
        _opcodes[0xF5].Execute = () => NOP();  // SBC Zero Page,X
        _opcodes[0xED].Execute = () => NOP();  // SBC Absolute
        _opcodes[0xFD].Execute = () => NOP();  // SBC Absolute,X
        _opcodes[0xF9].Execute = () => NOP();  // SBC Absolute,Y
        _opcodes[0xE1].Execute = () => NOP();  // SBC (Indirect,X)
        _opcodes[0xF1].Execute = () => NOP();  // SBC (Indirect),Y

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
        _opcodes[0x96].Execute = () => STX(ZeroPageIndirectY());  // STX Zero Page,Y
        _opcodes[0x8E].Execute = () => STX(Absolute());  // STX Absolute

        // Store Y Register
        _opcodes[0x84].Execute = () => STY(ZeroPage());  // STY Zero Page
        _opcodes[0x94].Execute = () => STY(ZeroPageIndirectX());  // STY Zero Page,X
        _opcodes[0x8C].Execute = () => STY(Absolute());  // STY Absolute
    }
}
