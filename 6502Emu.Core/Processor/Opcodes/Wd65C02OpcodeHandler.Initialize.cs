namespace Mos6502Emu.Core.Processor.Opcodes;

public partial class Wd65C02OpcodeHandler
{
    protected override void InitializeOpcodes()
    {
        // Initialize the opcodes for the WDC 65C02
        base.InitializeOpcodes();

        Add(new Opcode("BRA", "Relative", 0x80, 2, "Branch Always"));

        Add(new Opcode("ORA", "(Zero Page)", 0x12, 2, "Logical OR"));
        Add(new Opcode("AND", "(Zero Page)", 0x32, 2, "Logical AND"));
        Add(new Opcode("EOR", "(Zero Page)", 0x52, 2, "Exclusive OR"));
        Add(new Opcode("ADC", "(Zero Page)", 0x72, 2, "Add with Carry"));
        Add(new Opcode("STA", "(Zero Page)", 0x92, 2, "Store Accumulator"));
        Add(new Opcode("LDA", "(Zero Page)", 0xB2, 2, "Load Accumulator"));
        Add(new Opcode("CMP", "(Zero Page)", 0xD2, 2, "Compare"));
        Add(new Opcode("SBC", "(Zero Page)", 0xF2, 2, "Subtract with Carry"));

        Add(new Opcode("TSB", "Zero Page", 0x04, 2, "Test and Set Memory Bit"));
        Add(new Opcode("TRB", "Zero Page", 0x14, 2, "Test and Reset Memory Bit"));
        Add(new Opcode("TSB", "Absolute", 0x0C, 2, "Test and Set Memory Bit"));
        Add(new Opcode("TRB", "Absolute", 0x1C, 2, "Test and Reset Memory Bit"));

        Add(new Opcode("STZ", "Zero Page", 0x64, 2, "Store Zero in Memory"));
        Add(new Opcode("STZ", "Zero Page,X", 0x74, 2, "Store Zero in Memory"));
        Add(new Opcode("STZ", "Absolute", 0x9C, 3, "Store Zero in Memory"));
        Add(new Opcode("STZ", "Absolute,X", 0x9E, 3, "Store Zero in Memory"));

        Add(new Opcode("BIT", "Zero Page,X", 0x34, 2, "Bit Test"));
        Add(new Opcode("BIT", "Immediate", 0x89, 2, "Bit Test"));
        Add(new Opcode("BIT", "Absolute,X", 0x3C, 3, "Bit Test"));

        Add(new Opcode("JMP", "(Absolute,X)", 0x7C, 3, "Jump"));

        Add(new Opcode("RMB0", "Zero Page", 0x07, 2, "Reset Memory Bit #0"));
        Add(new Opcode("RMB1", "Zero Page", 0x17, 2, "Reset Memory Bit #1"));
        Add(new Opcode("RMB2", "Zero Page", 0x27, 2, "Reset Memory Bit #2"));
        Add(new Opcode("RMB3", "Zero Page", 0x37, 2, "Reset Memory Bit #3"));
        Add(new Opcode("RMB4", "Zero Page", 0x47, 2, "Reset Memory Bit #4"));
        Add(new Opcode("RMB5", "Zero Page", 0x57, 2, "Reset Memory Bit #5"));
        Add(new Opcode("RMB6", "Zero Page", 0x67, 2, "Reset Memory Bit #6"));
        Add(new Opcode("RMB7", "Zero Page", 0x77, 2, "Reset Memory Bit #7"));

        Add(new Opcode("SMB0", "Zero Page", 0x87, 2, "Set Memory Bit #0"));
        Add(new Opcode("SMB1", "Zero Page", 0x97, 2, "Set Memory Bit #1"));
        Add(new Opcode("SMB2", "Zero Page", 0xA7, 2, "Set Memory Bit #2"));
        Add(new Opcode("SMB3", "Zero Page", 0xB7, 2, "Set Memory Bit #3"));
        Add(new Opcode("SMB4", "Zero Page", 0xC7, 2, "Set Memory Bit #4"));
        Add(new Opcode("SMB5", "Zero Page", 0xD7, 2, "Set Memory Bit #5"));
        Add(new Opcode("SMB6", "Zero Page", 0xE7, 2, "Set Memory Bit #6"));
        Add(new Opcode("SMB7", "Zero Page", 0xF7, 2, "Set Memory Bit #7"));

        Add(new Opcode("INC", "Implied", 0x1A, 1, "Increment Accumulator"));
        Add(new Opcode("DEC", "Implied", 0x3A, 1, "Decrement Accumulator"));
        Add(new Opcode("PHY", "Implied", 0x5A, 1, "Push Y Register"));
        Add(new Opcode("PLY", "Implied", 0x7A, 1, "Pull Y Register"));
        Add(new Opcode("PHX", "Implied", 0xDA, 1, "Push X Register"));
        Add(new Opcode("PLX", "Implied", 0xFA, 1, "Pull X Register"));
        Add(new Opcode("WAI", "Implied", 0xCB, 1, "Wait for Interrupt"));
        Add(new Opcode("STP", "Implied", 0xDB, 1, "Stop Execution"));

        Add(new Opcode("BBR0", "Zero Page, Relative", 0x0F, 3, "Branch on Bit Reset"));
        Add(new Opcode("BBR1", "Zero Page, Relative", 0x1F, 3, "Branch on Bit Reset"));
        Add(new Opcode("BBR2", "Zero Page, Relative", 0x2F, 3, "Branch on Bit Reset"));
        Add(new Opcode("BBR3", "Zero Page, Relative", 0x3F, 3, "Branch on Bit Reset"));
        Add(new Opcode("BBR4", "Zero Page, Relative", 0x4F, 3, "Branch on Bit Reset"));
        Add(new Opcode("BBR5", "Zero Page, Relative", 0x5F, 3, "Branch on Bit Reset"));
        Add(new Opcode("BBR6", "Zero Page, Relative", 0x6F, 3, "Branch on Bit Reset"));
        Add(new Opcode("BBR7", "Zero Page, Relative", 0x7F, 3, "Branch on Bit Reset"));

        Add(new Opcode("BBS0", "Zero Page, Relative", 0x8F, 3, "Branch on Bit Set"));
        Add(new Opcode("BBS1", "Zero Page, Relative", 0x9F, 3, "Branch on Bit Set"));
        Add(new Opcode("BBS2", "Zero Page, Relative", 0xAF, 3, "Branch on Bit Set"));
        Add(new Opcode("BBS3", "Zero Page, Relative", 0xBF, 3, "Branch on Bit Set"));
        Add(new Opcode("BBS4", "Zero Page, Relative", 0xCF, 3, "Branch on Bit Set"));
        Add(new Opcode("BBS5", "Zero Page, Relative", 0xDF, 3, "Branch on Bit Set"));
        Add(new Opcode("BBS6", "Zero Page, Relative", 0xEF, 3, "Branch on Bit Set"));
        Add(new Opcode("BBS7", "Zero Page, Relative", 0xFF, 3, "Branch on Bit Set"));
    }
}
