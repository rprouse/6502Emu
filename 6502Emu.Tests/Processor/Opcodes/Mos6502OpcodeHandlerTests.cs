using System.Text.Json;
using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;
using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Tests.Processor.Opcodes;

public class Mos6502OpcodeHandlerTests
{
    const int TEST_PER_OPCODE = 10; // Number of test cases to run per opcode, max of 100

    Mmu _mmu;
    Mos6502Cpu _cpu;
    Mos6502OpcodeHandler _opcodeHandler;

    [SetUp]
    public void Setup()
    {
        // Initialize the CPU and memory
        _mmu = new Mmu();
        _cpu = new Mos6502Cpu(_mmu);

        _opcodeHandler = new Mos6502OpcodeHandler(_cpu.Registers, _mmu);
    }

    [TestCaseSource(nameof(GetOpcodes))]
    public void TestOpcodeIsImplemented(OpcodeTest testCase)
    {
        if (!Byte.TryParse(testCase.Name.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, null, out byte opcode))
            Assert.Fail($"Invalid opcode format: {testCase.Name}");

        try
        {
            var result = _opcodeHandler.GetOpcode((byte)testCase.Initial.RAM[0][1]);
            result.Execute.Should().NotBeNull(because: $"Opcode {testCase.Name} is not hooked up in the OpcodeHandler");
        }
        catch (NotImplementedException)
        {
            Assert.Fail($"Opcode {testCase.Name} is not implemented");
        }
    }

    [TestCaseSource(nameof(GetOpcodeTests))]
    public void TestOpcode(OpcodeTest testCase)
    {
        // Set initial state
        _cpu.Registers.PC = testCase.Initial.PC;
        _cpu.Registers.S = testCase.Initial.S;
        _cpu.Registers.A = testCase.Initial.A;
        _cpu.Registers.X = testCase.Initial.X;
        _cpu.Registers.Y = testCase.Initial.Y;
        _cpu.Registers.P = testCase.Initial.P;

        // Load initial RAM state
        foreach (var ram in testCase.Initial.RAM)
            _mmu[ram[0]] = (byte)ram[1];

        // Execute the opcode
        _opcodeHandler.FetchVerifyAndExecuteInstruction();

        // Validate final state
        _cpu.Registers.PC.Should().Be(testCase.Final.PC);
        _cpu.Registers.S.Should().Be(testCase.Final.S);
        _cpu.Registers.A.Should().Be(testCase.Final.A);
        _cpu.Registers.X.Should().Be(testCase.Final.X);
        _cpu.Registers.Y.Should().Be(testCase.Final.Y);
        _cpu.Registers.P.Should().Be(testCase.Final.P);

        // Validate RAM state
        foreach (var ram in testCase.Final.RAM)
            _mmu[ram[0]].Should().Be((byte)ram[1]);
    }

    public static IEnumerable<TestCaseData> GetOpcodeTests() =>
        LoadOpcodeTests("TestOpcode", TEST_PER_OPCODE);

    public static IEnumerable<TestCaseData> GetOpcodes() =>
        LoadOpcodeTests("TestOpcodeIsImplemented", 1);

    public static IEnumerable<TestCaseData> LoadOpcodeTests(string testName, int tests_per_opcode)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // Load test cases from JSON files for each opcode
        var files = Directory.GetFiles(@"OpcodeData/Mos6502/", "*.json");
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
