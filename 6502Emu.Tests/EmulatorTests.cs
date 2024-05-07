using Mos6502Emu.Core;
using Mos6502Emu.Core.Processor;

namespace Mos6502Emu.Tests;

public class EmulatorTests
{
    private Emulator _emulator;

    [SetUp]
    public void Setup()
    {
        _emulator = new Emulator();
    }


    [Test]
    public void ResetClearsCPUFlags()
    {
        _emulator.CPU.Registers.SetFlag(Flag.Carry);

        _emulator.Reset();

        _emulator.CPU.Registers.GetFlag(Flag.Carry).Should().BeFalse();
    }

    [Test]
    public void ResetClearsCPURegisters()
    {
        _emulator.CPU.Registers.A = 0xFF;

        _emulator.Reset();

        _emulator.CPU.Registers.A.Should().Be(0x00);
    }

    [Test]
    public void ResetClearsMemory()
    {
        _emulator.Memory[0x0100] = 0xFF;

        _emulator.Reset();

        _emulator.Memory[0x0100].Should().Be(0x00);
    }

}
