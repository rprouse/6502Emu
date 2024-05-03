// Open and read in "6502 Instructions.txt"
using System.Globalization;

var instructions = File.ReadAllLines("Files/6502 Instructions.txt")
    .Select(line => line.Split(' '))
    .Select(parts =>
    {
        byte b = (byte)Byte.Parse(parts[0], NumberStyles.HexNumber);
        string operand = string.Empty;
        if (parts.Length == 3)
        {
            operand = parts[2];
        }
        return new Opcode
        {
            Byte = b,
            Mnemonic = parts[1],
            Operand = operand
        };
    })
    .ToList();

instructions
    .ForEach(opcode => Console.WriteLine(opcode));


public class Opcode
{
    public byte Byte { get; set; }
    public string Mnemonic { get; set; }
    public string Operand { get; set; }
    public byte Bytes { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return $"{Byte} {Mnemonic} {Operand} {Bytes} {Description}";
    }
}   