namespace ParseOpcodes;

static class MarkDownOutput
{
    public static void WriteOpcodes(List<Opcode> instructions, string filename)
    {
        using var writer = new StreamWriter(filename);
        writer.WriteLine("# 65C02 Instructions");
        writer.WriteLine();
        writer.WriteLine("| Byte | Mnemonic | Bytes | Description |");
        writer.WriteLine("|------|----------|-------|-------------|");
        instructions
            .ForEach(opcode => writer.WriteLine(opcode.ToTableRowString()));
        writer.WriteLine();
    }
}
