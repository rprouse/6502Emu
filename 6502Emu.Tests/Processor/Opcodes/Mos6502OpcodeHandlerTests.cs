using System.Text.Json;
using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;
using Mos6502Emu.Core.Processor.Opcodes;

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
            var result = _opcodeHandler!.GetOpcode((byte)testCase.Initial.RAM[0][1]);
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
        _opcodeHandler!.FetchVerifyAndExecuteInstruction();

        // Validate final state
        _cpu.Registers.PC.ShouldBe(testCase.Final.PC);
        _cpu.Registers.S.ShouldBe(testCase.Final.S);
        _cpu.Registers.A.ShouldBe(testCase.Final.A);
        _cpu.Registers.X.ShouldBe(testCase.Final.X);
        _cpu.Registers.Y.ShouldBe(testCase.Final.Y);
        _cpu.Registers.P.ShouldBe(testCase.Final.P);

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

public class Mos6502OpcodeHandlerTests : OpcodeTestBase
{
    protected const int TESTS_PER_OPCODE = 10; // Number of test cases to run per opcode, max of 100
    const string TEST_DATA_DIR = @"OpcodeData/Mos6502/";

    [SetUp]
    public void Setup()
    {
        // Initialize the CPU and memory
        _mmu = new Mmu();
        _cpu = new Mos6502Cpu(_mmu);

        _opcodeHandler = new Mos6502OpcodeHandler(_cpu.Registers, _mmu);
    }

    [TestCaseSource(nameof(GetOpcodes))]
    public override void TestOpcodeIsImplemented(OpcodeTest testCase)
    {
        base.TestOpcodeIsImplemented(testCase);
    }

    [TestCaseSource(nameof(GetOpcodeTests))]
    public override void TestOpcode(OpcodeTest testCase)
    {
        base.TestOpcode(testCase);
    }

    public static IEnumerable<TestCaseData> GetOpcodeTests() =>
        LoadOpcodeTests(TEST_DATA_DIR, "TestOpcode", TESTS_PER_OPCODE);

    public static IEnumerable<TestCaseData> GetOpcodes() =>
        LoadOpcodeTests(TEST_DATA_DIR, "TestOpcodeIsImplemented", 1);
}
