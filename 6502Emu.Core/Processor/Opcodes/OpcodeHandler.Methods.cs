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

        // Arithmetic Shift Left
        _opcodes[0x0A].Execute = () => NOP();  // ASL Accumulator
        _opcodes[0x06].Execute = () => NOP();  // ASL Zero Page 
        _opcodes[0x16].Execute = () => NOP();  // ASL Zero Page,X
        _opcodes[0x0E].Execute = () => NOP();  // ASL Absolute
        _opcodes[0x1E].Execute = () => NOP();  // ASL Absolute,X

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

        // Compare
        _opcodes[0xC9].Execute = () => NOP();  // CMP Immediate 
        _opcodes[0xC5].Execute = () => NOP();  // CMP Zero Page 
        _opcodes[0xD5].Execute = () => NOP();  // CMP Zero Page,X
        _opcodes[0xCD].Execute = () => NOP();  // CMP Absolute
        _opcodes[0xDD].Execute = () => NOP();  // CMP Absolute,X
        _opcodes[0xD9].Execute = () => NOP();  // CMP Absolute,Y
        _opcodes[0xC1].Execute = () => NOP();  // CMP (Indirect,X)
        _opcodes[0xD1].Execute = () => NOP();  // CMP (Indirect),Y
    }
}
