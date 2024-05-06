namespace ParseOpcodes;

// Open and read in "6502 Instructions.txt"
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Opcode
{
    public byte Byte { get; set; }
    public string Mnemonic { get; set; }
    public string Operand { get; set; }
    public byte Bytes { get; set; }
    public string Description { get; set; }

    public string ToTableRowString()
    {
        return $"| 0x{Byte:X2} | {Mnemonic} {Operand} | {Bytes} | {Description} |";
    }
}   
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
