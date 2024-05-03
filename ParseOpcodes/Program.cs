using System.Globalization;
using ParseOpcodes;

// Read in the descriptions of the 6502 opcodes
var descriptions = File.ReadAllLines("Files/6502 Instruction Set Table.txt")
    .Select(line => line.Split(' ', 2))
    .ToDictionary(parts => parts[0], parts => parts[1]);

// Read in the length of the opcodes
var lengths = File.ReadAllLines("Files/6502 Address Modes.txt")
    .Select(line => line.Split(' '))
    .ToDictionary(parts => parts[0], parts => byte.Parse(parts[1]));

// Open and read in "6502 Instructions.txt"
var instructions = File.ReadAllLines("Files/6502 Instructions.txt")
    .Select(line => line.Split(' '))
    .Select(parts =>
    {
        byte b = byte.Parse(parts[0], NumberStyles.HexNumber);
        string operand = string.Empty;
        byte bytes = 0;
        if (parts.Length == 3)
        {
            operand = parts[2];
            bytes = lengths[parts[2]];
        }
        return new Opcode
        {
            Byte = b,
            Mnemonic = parts[1],
            Operand = operand,
            Description = descriptions[parts[1].Substring(0, 3)],
            Bytes = bytes
        };
    })
    .ToList();

instructions
    .ForEach(opcode => Console.WriteLine(opcode));
