using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;
using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Tests.Processor.Opcodes;

public class W65C02SOpcodeHandlerTests : OpcodeTestBase
{
    protected const int TESTS_PER_OPCODE = 4; // Number of test cases to run per opcode, max of 100
    const string TEST_DATA_DIR = @"OpcodeData/W65C02S/";

    [SetUp]
    public void Setup()
    {
        // Initialize the CPU and memory
        _mmu = new Mmu();
        _cpu = new W65C02SCpu(_mmu);

        _opcodeHandler = new W65C02SOpcodeHandler(_cpu.Registers, _mmu);
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
