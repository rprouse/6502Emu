using System.Text.Json;
using Mos6502Emu.Tests.Processor.Opcodes;

string inputDir = @"D:\src\Retro\6502\65x02\wdc65c02\v1\";
string outputDir = @"D:\src\Retro\6502\6502Emu\6502Emu.Tests\OpcodeData\Wd65C02";

var illegalOpcodes = new []
{
    0x02, 0x03, 0x0B,
    0x13, 0x1B,
    0x22, 0x23, 0x2B,
    0x33, 0x3B,
    0x42, 0x43, 0x44, 0x4B,
    0x53, 0x54, 0x5B, 0x5C,
    0x62, 0x63, 0x6B,
    0x73, 0x7B,
    0x82, 0x83, 0x8B,
    0x93, 0x9B,
    0xA3, 0xAB,
    0xB3, 0xBB,
    0xC2, 0xC3,
    0xD3, 0xD4, 0xDC,
    0xE2, 0xE3, 0xEB,
    0xF3, 0xF4, 0xFB, 0xFC,
};

var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

// Load test cases from JSON files for each opcode
var files = Directory.GetFiles(inputDir, "*.json");
foreach (var file in files)
{
    var fileName = Path.GetFileName(file);

    // Skip files for illegal opcodes
    if (illegalOpcodes.Any(op => fileName.Substring(0, 2).ToLowerInvariant() == op.ToString("x2")))
    {
        Console.WriteLine($"INFO: Skipping illegal opcode {fileName}.");
        continue;
    }

    var json = File.ReadAllText(file);
    List<OpcodeTest>? testCases = null;

    try
    {
        testCases = JsonSerializer.Deserialize<List<OpcodeTest>>(json, options);
        if (testCases == null)
        {
            Console.WriteLine($"ERROR: Failed to deserialize test cases from {file}");
            continue;
        }
    }
    catch (JsonException ex)
    {
        Console.WriteLine($"ERROR: Failed to deserialize test cases from {file}: {ex.Message}");
        continue;
    }

    // Save out to the output directory
    var outputFile = Path.Combine(outputDir, fileName);
    var outputJson = JsonSerializer.Serialize(testCases.Take(100), options);
    File.WriteAllText(outputFile, outputJson);
    Console.WriteLine($"Successfully wrote to {outputFile}");
}
