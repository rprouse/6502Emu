using System.Text.Json;
using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;
using Mos6502Emu.Core.Processor.Opcodes;
using Mos6502Emu.Core.Utilities;

namespace Mos6502Emu.Tests.Processor.Opcodes;

public abstract class OpcodeTestBase
{
    protected Mmu? _mmu;
    protected ICpu? _cpu;
    protected IOpcodeHandler? _opcodeHandler;

    public virtual void TestOpcodeIsImplemented(OpcodeTest testCase)
    {
        if (!Byte.TryParse(testCase.Name.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, null, out byte opcode))
            Assert.Fail($"Invalid opcode format: {testCase.Name}");

        try
        {
            var result = _opcodeHandler!.GetOpcode(testCase.Initial[testCase.Initial.PC]);
            result.Execute.ShouldNotBeNull($"Opcode {testCase.Name} is not hooked up in the OpcodeHandler");
        }
        catch (NotImplementedException)
        {
            Assert.Fail($"Opcode {testCase.Name} is not implemented");
        }
    }

    public virtual void TestOpcode(OpcodeTest testCase)
    {
        // Set initial state
        _cpu!.Registers.PC = testCase.Initial.PC;
        _cpu.Registers.S = testCase.Initial.S;
        _cpu.Registers.A = testCase.Initial.A;
        _cpu.Registers.X = testCase.Initial.X;
        _cpu.Registers.Y = testCase.Initial.Y;
        _cpu.Registers.P = testCase.Initial.P;

        // Load initial RAM state
        foreach (var ram in testCase.Initial.RAM)
            _mmu![ram[0]] = (byte)ram[1];

        // Execute the opcode
        Opcode opcode = _opcodeHandler!.FetchVerifyAndExecuteInstruction();

        // Validate final state
        _cpu.Registers.PC.ShouldBe(testCase.Final.PC, $"{opcode.Mnemonic} PC should be {testCase.Final.PC.ToHexString()} but was {_cpu.Registers.PC.ToHexString()}");
        _cpu.Registers.S.ShouldBe(testCase.Final.S, $"{opcode.Mnemonic} S should be {testCase.Final.S.ToHexString()} but was {_cpu.Registers.S.ToHexString()}");
        _cpu.Registers.A.ShouldBe(testCase.Final.A, $"{opcode.Mnemonic} A should be {testCase.Final.A.ToHexString()} but was {_cpu.Registers.A.ToHexString()}");
        _cpu.Registers.X.ShouldBe(testCase.Final.X, $"{opcode.Mnemonic} X should be {testCase.Final.X.ToHexString()} but was {_cpu.Registers.X.ToHexString()}");
        _cpu.Registers.Y.ShouldBe(testCase.Final.Y, $"{opcode.Mnemonic} Y should be {testCase.Final.Y.ToHexString()} but was {_cpu.Registers.Y.ToHexString()}");
        _cpu.Registers.P.ShouldBe(testCase.Final.P, $"{opcode.Mnemonic} P (NV__-DIZC) should be {testCase.Final.P.ToBinaryString()} but was {_cpu.Registers.P.ToBinaryString()}");

        // Validate RAM state
        foreach (var ram in testCase.Final.RAM)
            _mmu![ram[0]].ShouldBe((byte)ram[1]);
    }

    public static IEnumerable<TestCaseData> LoadOpcodeTests(string testDataDir, string testName, int tests_per_opcode)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // Load test cases from JSON files for each opcode
        var files = Directory.GetFiles(testDataDir, "*.json");
        foreach (var file in files)
        {
            var json = File.ReadAllText(file);
            var testCases = JsonSerializer.Deserialize<List<OpcodeTest>>(json, options);
            if (testCases == null)
                throw new Exception($"Failed to deserialize test cases from {file}");

            foreach (var testCase in testCases.Take(tests_per_opcode))
            {
                yield return new TestCaseData(testCase).SetName($"{testName}(0x{testCase.Name.Substring(0, 2):2h}, {testCase.Name.Substring(3)})");
            }
        }
    }
}
