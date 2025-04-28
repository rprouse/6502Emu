using Mos6502Emu.Core.Processor;

namespace Mos6502Emu.Tests.Processor;

public class RegistersTests
{
    Registers _reg;

    [SetUp]
    public void Setup()
    {
        _reg = new Registers();
    }

    [TestCaseSource(nameof(FlagData))]
    public void TestSetFlagNegative(Flag flag, byte initial)
    {
        _reg.P = initial;
        _reg.GetFlag(flag).Should().Be(false);
        _reg.SetFlag(flag);
        _reg.GetFlag(flag).Should().Be(true);
        _reg.P.Should().Be(0b1111_1111);
    }

    [TestCaseSource(nameof(FlagData))]
    public void TestResetFlagNegative(Flag flag, byte expected)
    {
        _reg.P = 0b1111_1111;
        _reg.GetFlag(flag).Should().Be(true);
        _reg.ResetFlag(flag);
        _reg.GetFlag(flag).Should().Be(false);
        _reg.P.Should().Be(expected);
    }

    public static IEnumerable<object[]> FlagData =>
        new List<object[]>
        {
            new object[] { Flag.Negative, (byte)0b0111_1111 },
            new object[] { Flag.Overflow, (byte)0b1011_1111 },
            new object[] { Flag.Break, (byte)0b1110_1111 },
            new object[] { Flag.Decimal, (byte)0b1111_0111 },
            new object[] { Flag.Interupt, (byte)0b1111_1011 },
            new object[] { Flag.Zero, (byte)0b1111_1101 },
            new object[] { Flag.Carry, (byte)0b1111_1110 }
        };
}
