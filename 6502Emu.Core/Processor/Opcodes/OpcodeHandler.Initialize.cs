namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class OpcodeHandler
{
    private void IntializeOpcodes()
    {
        // Add with Carry
        Add(new Opcode("ADC", "Immediate", 0x69, 2, "Add with Carry"));
        Add(new Opcode("ADC", "Zero Page", 0x65, 2, "Add with Carry"));
        Add(new Opcode("ADC", "Zero Page,X", 0x75, 2, "Add with Carry"));
        Add(new Opcode("ADC", "Absolute", 0x6D, 3, "Add with Carry"));
        Add(new Opcode("ADC", "Absolute,X", 0x7D, 3, "Add with Carry"));
        Add(new Opcode("ADC", "Absolute,Y", 0x79, 3, "Add with Carry"));
        Add(new Opcode("ADC", "(Indirect,X)", 0x61, 2, "Add with Carry"));
        Add(new Opcode("ADC", "(Indirect),Y", 0x71, 2, "Add with Carry"));

        // Logical AND
        Add(new Opcode("AND", "Immediate", 0x29, 2, "Logical AND"));
        Add(new Opcode("AND", "Zero Page", 0x25, 2, "Logical AND"));
        Add(new Opcode("AND", "Zero Page,X", 0x35, 2, "Logical AND"));
        Add(new Opcode("AND", "Absolute", 0x2D, 3, "Logical AND"));
        Add(new Opcode("AND", "Absolute,X", 0x3D, 3, "Logical AND"));
        Add(new Opcode("AND", "Absolute,Y", 0x39, 3, "Logical AND"));
        Add(new Opcode("AND", "(Indirect,X)", 0x21, 2, "Logical AND"));
        Add(new Opcode("AND", "(Indirect),Y", 0x31, 2, "Logical AND"));

        // Exclusive OR
        Add(new Opcode("EOR", "Immediate", 0x49, 2, "Exclusive OR"));
        Add(new Opcode("EOR", "Zero Page", 0x45, 2, "Exclusive OR"));
        Add(new Opcode("EOR", "Zero Page,X", 0x55, 2, "Exclusive OR"));
        Add(new Opcode("EOR", "Absolute", 0x4D, 3, "Exclusive OR"));
        Add(new Opcode("EOR", "Absolute,X", 0x5D, 3, "Exclusive OR"));
        Add(new Opcode("EOR", "Absolute,Y", 0x59, 3, "Exclusive OR"));
        Add(new Opcode("EOR", "(Indirect,X)", 0x41, 2, "Exclusive OR"));
        Add(new Opcode("EOR", "(Indirect),Y", 0x51, 2, "Exclusive OR"));

        // Logical OR
        Add(new Opcode("ORA", "Immediate", 0x09, 2, "Logical OR"));
        Add(new Opcode("ORA", "Zero Page", 0x05, 2, "Logical OR"));
        Add(new Opcode("ORA", "Zero Page,X", 0x15, 2, "Logical OR"));
        Add(new Opcode("ORA", "Absolute", 0x0D, 3, "Logical OR"));
        Add(new Opcode("ORA", "Absolute,X", 0x1D, 3, "Logical OR"));
        Add(new Opcode("ORA", "Absolute,Y", 0x19, 3, "Logical OR"));
        Add(new Opcode("ORA", "(Indirect,X)", 0x01, 2, "Logical OR"));
        Add(new Opcode("ORA", "(Indirect),Y", 0x11, 2, "Logical OR"));

        // Arithmetic Shift Left
        Add(new Opcode("ASL", "Accumulator", 0x0A, 1, "Arithmetic Shift Left"));
        Add(new Opcode("ASL", "Zero Page", 0x06, 2, "Arithmetic Shift Left"));
        Add(new Opcode("ASL", "Zero Page,X", 0x16, 2, "Arithmetic Shift Left"));
        Add(new Opcode("ASL", "Absolute", 0x0E, 3, "Arithmetic Shift Left"));
        Add(new Opcode("ASL", "Absolute,X", 0x1E, 3, "Arithmetic Shift Left"));

        // Logical Shift Right
        Add(new Opcode("LSR", "Accumulator", 0x4A, 1, "Logical Shift Right"));
        Add(new Opcode("LSR", "Zero Page", 0x46, 2, "Logical Shift Right"));
        Add(new Opcode("LSR", "Zero Page,X", 0x56, 2, "Logical Shift Right"));
        Add(new Opcode("LSR", "Absolute", 0x4E, 3, "Logical Shift Right"));
        Add(new Opcode("LSR", "Absolute,X", 0x5E, 3, "Logical Shift Right"));

        // Rotate Left
        Add(new Opcode("ROL", "Accumulator", 0x2A, 1, "Rotate Left"));
        Add(new Opcode("ROL", "Zero Page", 0x26, 2, "Rotate Left"));
        Add(new Opcode("ROL", "Zero Page,X", 0x36, 2, "Rotate Left"));
        Add(new Opcode("ROL", "Absolute", 0x2E, 3, "Rotate Left"));
        Add(new Opcode("ROL", "Absolute,X", 0x3E, 3, "Rotate Left"));

        // Rotate Right
        Add(new Opcode("ROR", "Accumulator", 0x6A, 1, "Rotate Right"));
        Add(new Opcode("ROR", "Zero Page", 0x66, 2, "Rotate Right"));
        Add(new Opcode("ROR", "Zero Page,X", 0x76, 2, "Rotate Right"));
        Add(new Opcode("ROR", "Absolute", 0x6E, 3, "Rotate Right"));
        Add(new Opcode("ROR", "Absolute,X", 0x7E, 3, "Rotate Right"));

        // Decrement Memory
        Add(new Opcode("DEC", "Zero Page", 0xC6, 2, "Decrement Memory"));
        Add(new Opcode("DEC", "Zero Page,X", 0xD6, 2, "Decrement Memory"));
        Add(new Opcode("DEC", "Absolute", 0xCE, 3, "Decrement Memory"));
        Add(new Opcode("DEC", "Absolute,X", 0xDE, 3, "Decrement Memory"));

        // Increment Memory
        Add(new Opcode("INC", "Zero Page", 0xE6, 2, "Increment Memory"));
        Add(new Opcode("INC", "Zero Page,X", 0xF6, 2, "Increment Memory"));
        Add(new Opcode("INC", "Absolute", 0xEE, 3, "Increment Memory"));
        Add(new Opcode("INC", "Absolute,X", 0xFE, 3, "Increment Memory"));

        // Increment/Decrement Register Instructions
        Add(new Opcode("INX", "Implied", 0xE8, 1, "Increment X Register"));
        Add(new Opcode("DEX", "Implied", 0xCA, 1, "Decrement X Register"));
        Add(new Opcode("INY", "Implied", 0xC8, 1, "Increment Y Register"));
        Add(new Opcode("DEY", "Implied", 0x88, 1, "Decrement Y Register"));

        // No Operation
        Add(new Opcode("NOP", "Implied", 0xEA, 1, "No Operation"));

        // Branch Instructions
        Add(new Opcode("BCC", "Relative", 0x90, 2, "Branch if Carry Clear"));
        Add(new Opcode("BCS", "Relative", 0xB0, 2, "Branch if Carry Set"));
        Add(new Opcode("BEQ", "Relative", 0xF0, 2, "Branch if Equal"));
        Add(new Opcode("BMI", "Relative", 0x30, 2, "Branch if Minus"));
        Add(new Opcode("BNE", "Relative", 0xD0, 2, "Branch if Not Equal"));
        Add(new Opcode("BPL", "Relative", 0x10, 2, "Branch if Positive"));
        Add(new Opcode("BVC", "Relative", 0x50, 2, "Branch if Overflow Clear"));
        Add(new Opcode("BVS", "Relative", 0x70, 2, "Branch if Overflow Set"));

        // Bit Test
        Add(new Opcode("BIT", "Zero Page", 0x24, 2, "Bit Test"));
        Add(new Opcode("BIT", "Absolute", 0x2C, 3, "Bit Test"));

        // Break
        Add(new Opcode("BRK", "Implied", 0x00, 1, "Force Interrupt"));

        // Clear Flags
        Add(new Opcode("CLC", "Implied", 0x18, 1, "Clear Carry Flag"));
        Add(new Opcode("CLD", "Implied", 0xD8, 1, "Clear Decimal Mode"));
        Add(new Opcode("CLI", "Implied", 0x58, 1, "Clear Interrupt Disable"));
        Add(new Opcode("CLV", "Implied", 0xB8, 1, "Clear Overflow Flag"));

        // Set Flags
        Add(new Opcode("SEC", "Implied", 0x38, 1, "Set Carry Flag"));
        Add(new Opcode("SED", "Implied", 0xF8, 1, "Set Decimal Mode"));
        Add(new Opcode("SEI", "Implied", 0x78, 1, "Set Interrupt Disable"));

        // Jump and Call Instructions
        Add(new Opcode("JSR", "Absolute", 0x20, 3, "Jump to Subroutine"));
        Add(new Opcode("RTS", "Implied", 0x60, 1, "Return from Subroutine"));
        Add(new Opcode("RTI", "Implied", 0x40, 1, "Return from Interrupt"));
        Add(new Opcode("JMP", "Absolute", 0x4C, 3, "Jump"));
        Add(new Opcode("JMP", "Indirect", 0x6C, 3, "Jump"));

        // Compare
        Add(new Opcode("CMP", "Immediate", 0xC9, 2, "Compare"));
        Add(new Opcode("CMP", "Zero Page", 0xC5, 2, "Compare"));
        Add(new Opcode("CMP", "Zero Page,X", 0xD5, 2, "Compare"));
        Add(new Opcode("CMP", "Absolute", 0xCD, 3, "Compare"));
        Add(new Opcode("CMP", "Absolute,X", 0xDD, 3, "Compare"));
        Add(new Opcode("CMP", "Absolute,Y", 0xD9, 3, "Compare"));
        Add(new Opcode("CMP", "(Indirect,X)", 0xC1, 2, "Compare"));
        Add(new Opcode("CMP", "(Indirect),Y", 0xD1, 2, "Compare"));

        // Compare X Register
        Add(new Opcode("CPX", "Immediate", 0xE0, 2, "Compare X Register"));
        Add(new Opcode("CPX", "Zero Page", 0xE4, 2, "Compare X Register"));
        Add(new Opcode("CPX", "Absolute", 0xEC, 3, "Compare X Register"));

        // Compare Y Register
        Add(new Opcode("CPY", "Immediate", 0xC0, 2, "Compare Y Register"));
        Add(new Opcode("CPY", "Zero Page", 0xC4, 2, "Compare Y Register"));
        Add(new Opcode("CPY", "Absolute", 0xCC, 3, "Compare Y Register"));

        // Load Accumulator
        Add(new Opcode("LDA", "Immediate", 0xA9, 2, "Load Accumulator"));
        Add(new Opcode("LDA", "Zero Page", 0xA5, 2, "Load Accumulator"));
        Add(new Opcode("LDA", "Zero Page,X", 0xB5, 2, "Load Accumulator"));
        Add(new Opcode("LDA", "Absolute", 0xAD, 3, "Load Accumulator"));
        Add(new Opcode("LDA", "Absolute,X", 0xBD, 3, "Load Accumulator"));
        Add(new Opcode("LDA", "Absolute,Y", 0xB9, 3, "Load Accumulator"));
        Add(new Opcode("LDA", "(Indirect,X)", 0xA1, 2, "Load Accumulator"));
        Add(new Opcode("LDA", "(Indirect),Y", 0xB1, 2, "Load Accumulator"));

        // Load X Register
        Add(new Opcode("LDX", "Immediate", 0xA2, 2, "Load X Register"));
        Add(new Opcode("LDX", "Zero Page", 0xA6, 2, "Load X Register"));
        Add(new Opcode("LDX", "Zero Page,Y", 0xB6, 2, "Load X Register"));
        Add(new Opcode("LDX", "Absolute", 0xAE, 3, "Load X Register"));
        Add(new Opcode("LDX", "Absolute,Y", 0xBE, 3, "Load X Register"));

        // Load Y Register
        Add(new Opcode("LDY", "Immediate", 0xA0, 2, "Load Y Register"));
        Add(new Opcode("LDY", "Zero Page", 0xA4, 2, "Load Y Register"));
        Add(new Opcode("LDY", "Zero Page,X", 0xB4, 2, "Load Y Register"));
        Add(new Opcode("LDY", "Absolute", 0xAC, 3, "Load Y Register"));
        Add(new Opcode("LDY", "Absolute,X", 0xBC, 3, "Load Y Register"));

        // Transfer Instructions
        Add(new Opcode("TSX", "Implied", 0xBA, 1, "Transfer Stack Pointer to X"));
        Add(new Opcode("TXA", "Implied", 0x8A, 1, "Transfer X to Accumulator"));
        Add(new Opcode("TYA", "Implied", 0x98, 1, "Transfer Y to Accumulator"));
        Add(new Opcode("TAX", "Implied", 0xAA, 1, "Transfer Accumulator to X"));
        Add(new Opcode("TAY", "Implied", 0xA8, 1, "Transfer Accumulator to Y"));
        Add(new Opcode("TXS", "Implied", 0x9A, 1, "Transfer X to Stack Pointer"));

        // Stack Operations
        Add(new Opcode("PLA", "Implied", 0x68, 1, "Pull Accumulator from Stack"));
        Add(new Opcode("PHA", "Implied", 0x48, 1, "Push Accumulator on Stack"));
        Add(new Opcode("PLP", "Implied", 0x28, 1, "Pull Processor Status from Stack"));
        Add(new Opcode("PHP", "Implied", 0x08, 1, "Push Processor Status on Stack"));

        // Subtract with Carry
        Add(new Opcode("SBC", "Immediate", 0xE9, 2, "Subtract with Carry"));
        Add(new Opcode("SBC", "Zero Page", 0xE5, 2, "Subtract with Carry"));
        Add(new Opcode("SBC", "Zero Page,X", 0xF5, 2, "Subtract with Carry"));
        Add(new Opcode("SBC", "Absolute", 0xED, 3, "Subtract with Carry"));
        Add(new Opcode("SBC", "Absolute,X", 0xFD, 3, "Subtract with Carry"));
        Add(new Opcode("SBC", "Absolute,Y", 0xF9, 3, "Subtract with Carry"));
        Add(new Opcode("SBC", "(Indirect,X)", 0xE1, 2, "Subtract with Carry"));
        Add(new Opcode("SBC", "(Indirect),Y", 0xF1, 2, "Subtract with Carry"));

        // Store Accumulator
        Add(new Opcode("STA", "Zero Page", 0x85, 2, "Store Accumulator"));
        Add(new Opcode("STA", "Zero Page,X", 0x95, 2, "Store Accumulator"));
        Add(new Opcode("STA", "Absolute", 0x8D, 3, "Store Accumulator"));
        Add(new Opcode("STA", "Absolute,X", 0x9D, 3, "Store Accumulator"));
        Add(new Opcode("STA", "Absolute,Y", 0x99, 3, "Store Accumulator"));
        Add(new Opcode("STA", "(Indirect,X)", 0x81, 2, "Store Accumulator"));
        Add(new Opcode("STA", "(Indirect),Y", 0x91, 2, "Store Accumulator"));

        // Store X Register
        Add(new Opcode("STX", "Zero Page", 0x86, 2, "Store X Register"));
        Add(new Opcode("STX", "Zero Page,Y", 0x96, 2, "Store X Register"));
        Add(new Opcode("STX", "Absolute", 0x8E, 3, "Store X Register"));

        // Store Y Register
        Add(new Opcode("STY", "Zero Page", 0x84, 2, "Store Y Register"));
        Add(new Opcode("STY", "Zero Page,X", 0x94, 2, "Store Y Register"));
        Add(new Opcode("STY", "Absolute", 0x8C, 3, "Store Y Register"));
    }
}
