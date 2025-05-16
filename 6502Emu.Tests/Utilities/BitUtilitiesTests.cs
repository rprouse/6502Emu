using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mos6502Emu.Core.Utilities;

namespace Mos6502Emu.Tests.Utilities;

public class BitUtilitiesTests
{
    [Test]
    public void TestLsb()
    {
        word b = 0xABCD;
        b.Lsb().ShouldBe(0xCD);
    }

    [Test]
    public void TestMsb()
    {
        word b = 0xABCD;
        b.Msb().ShouldBe(0xAB);
    }

    [Test]
    public void TestToWord()
    {
        BitUtilities.ToWord(0xAB, 0xCD).ShouldBe(0xABCD);
    }

    [TestCase(0, 0b_0000_0001, true)]
    [TestCase(1, 0b_0000_0010, true)]
    [TestCase(2, 0b_0000_0100, true)]
    [TestCase(3, 0b_0000_1000, true)]
    [TestCase(4, 0b_0001_0000, true)]
    [TestCase(5, 0b_0010_0000, true)]
    [TestCase(6, 0b_0100_0000, true)]
    [TestCase(7, 0b_1000_0000, true)]
    [TestCase(0, 0b_1111_1110, false)]
    [TestCase(1, 0b_1111_1101, false)]
    [TestCase(2, 0b_1111_1011, false)]
    [TestCase(3, 0b_1111_0111, false)]
    [TestCase(4, 0b_1110_1111, false)]
    [TestCase(5, 0b_1101_1111, false)]
    [TestCase(6, 0b_1011_1111, false)]
    [TestCase(7, 0b_0111_1111, false)]
    public void TestIsBitSet(int bit, byte b, bool expected)
    {
        b.IsBitSet(bit).ShouldBe(expected);
    }

    [TestCase(0, 0b_0000_0001)]
    [TestCase(1, 0b_0000_0010)]
    [TestCase(2, 0b_0000_0100)]
    [TestCase(3, 0b_0000_1000)]
    [TestCase(4, 0b_0001_0000)]
    [TestCase(5, 0b_0010_0000)]
    [TestCase(6, 0b_0100_0000)]
    [TestCase(7, 0b_1000_0000)]
    public void TestSetBit(int bit, byte expected)
    {
        byte b = 0b_0000_0000;
        b.SetBit(bit).ShouldBe(expected);
    }

    [TestCase(0, 0b_1111_1110)]
    [TestCase(1, 0b_1111_1101)]
    [TestCase(2, 0b_1111_1011)]
    [TestCase(3, 0b_1111_0111)]
    [TestCase(4, 0b_1110_1111)]
    [TestCase(5, 0b_1101_1111)]
    [TestCase(6, 0b_1011_1111)]
    [TestCase(7, 0b_0111_1111)]
    public void TestResetBit(int bit, byte expected)
    {
        byte b = 0b_1111_1111;
        b.ResetBit(bit).ShouldBe(expected);
    }

    [TestCase(0b0000_0000, false)]
    [TestCase(0b0111_1111, false)]
    [TestCase(0b1000_0000, true)]
    [TestCase(0b1111_1111, true)]
    public void TestIsNegative(byte b, bool expected)
    {
        b.IsNegative().ShouldBe(expected);
    }

    [TestCase(0b0000_0000_0000, false)]
    [TestCase(0b0000_0111_1111, false)]
    [TestCase(0b0000_1000_0000, true)]
    [TestCase(0b0000_1111_1111, true)]
    [TestCase(0b0001_0000_0000, false)]
    [TestCase(0b0001_0111_1111, false)]
    [TestCase(0b0001_1000_0000, true)]
    [TestCase(0b0001_1111_1111, true)]
    public void TestIsNegative(int b, bool expected)
    {
        b.IsNegative().ShouldBe(expected);
    }
}
