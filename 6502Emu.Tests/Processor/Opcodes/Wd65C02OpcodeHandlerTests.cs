using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;
using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Tests.Processor.Opcodes;

public class Wd65C02OpcodeHandlerTests : OpcodeTestBase
{
    protected const int TESTS_PER_OPCODE = 4; // Number of test cases to run per opcode, max of 100
    const string TEST_DATA_DIR = @"OpcodeData/Wd65C02/";

    [SetUp]
    public void Setup()
    {
        // Initialize the CPU and memory
        _mmu = new Mmu();
        _cpu = new Wd65C02Cpu(_mmu);

        _opcodeHandler = new Wd65C02OpcodeHandler(_cpu.Registers, _mmu);
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
