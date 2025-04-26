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

        // Arithmetic Shift Left
        Add(new Opcode("ASL", "Accumulator", 0x0A, 1, "Arithmetic Shift Left"));
        Add(new Opcode("ASL", "Zero Page", 0x06, 2, "Arithmetic Shift Left"));
        Add(new Opcode("ASL", "Zero Page,X", 0x16, 2, "Arithmetic Shift Left"));
        Add(new Opcode("ASL", "Absolute", 0x0E, 3, "Arithmetic Shift Left"));
        Add(new Opcode("ASL", "Absolute,X", 0x1E, 3, "Arithmetic Shift Left"));

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

        // Compare
        Add(new Opcode("CMP", "Immediate", 0xC9, 2, "Compare"));
        Add(new Opcode("CMP", "Zero Page", 0xC5, 2, "Compare"));
        Add(new Opcode("CMP", "Zero Page,X", 0xD5, 2, "Compare"));
        Add(new Opcode("CMP", "Absolute", 0xCD, 3, "Compare"));
        Add(new Opcode("CMP", "Absolute,X", 0xDD, 3, "Compare"));
        Add(new Opcode("CMP", "Absolute,Y", 0xD9, 3, "Compare"));
        Add(new Opcode("CMP", "(Indirect,X)", 0xC1, 2, "Compare"));
        Add(new Opcode("CMP", "(Indirect),Y", 0xD1, 2, "Compare"));
    }
}
