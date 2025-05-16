using Mos6502Emu.Core;
using Mos6502Emu.Core.Processor;

namespace Mos6502Emu.Tests;

public class EmulatorTests
{
    private Emulator _emulator;

    [SetUp]
    public void Setup()
    {
        _emulator = new Emulator(CpuType.MOS6502);
    }

    [Test]
    public void ResetClearsWarmBoot()
    {
        _emulator.WarmBoot = true;

        _emulator.Reset();

        _emulator.WarmBoot.ShouldBeFalse();
    }


    [Test]
    public void ResetClearsCPUFlags()
    {
        _emulator.CPU.Registers.SetFlag(Flag.Carry);

        _emulator.Reset();

        _emulator.CPU.Registers.GetFlag(Flag.Carry).ShouldBeFalse();
    }

    [Test]
    public void ResetClearsCPURegisters()
    {
        _emulator.CPU.Registers.A = 0xFF;

        _emulator.Reset();

        _emulator.CPU.Registers.A.ShouldBe(0x00);
    }

    [Test]
    public void ResetClearsMemory()
    {
        _emulator.Memory[0x8000] = 0xFF;

        _emulator.Reset();

        _emulator.Memory[0x8000].ShouldBe(0x00);
    }

    [Test]
    public void CanLoadProgramToDefaultAddress()
    {
        _emulator.LoadProgram("Test.prg");

        _emulator.Memory[0x8000].ShouldBe(0xA9);
        _emulator.Memory[0x8001].ShouldBe(0xDE);
        _emulator.Memory[0x8002].ShouldBe(0x69);
        _emulator.Memory[0x8003].ShouldBe(0x2A);
        _emulator.Memory[0x8004].ShouldBe(0x85);
        _emulator.Memory[0x8005].ShouldBe(0x00);
        _emulator.Memory[0x8006].ShouldBe(0xC6);
        _emulator.Memory[0x8007].ShouldBe(0x00);
        _emulator.Memory[0x8008].ShouldBe(0x60);
    }

    [Test]
    public void CanLoadProgramAtSpecifiedAddress()
    {
        _emulator.LoadProgram("Test.prg", 0x0200);

        _emulator.Memory[0x0200].ShouldBe(0xA9);
        _emulator.Memory[0x0201].ShouldBe(0xDE);
        _emulator.Memory[0x0202].ShouldBe(0x69);
        _emulator.Memory[0x0203].ShouldBe(0x2A);
        _emulator.Memory[0x0204].ShouldBe(0x85);
        _emulator.Memory[0x0205].ShouldBe(0x00);
        _emulator.Memory[0x0206].ShouldBe(0xC6);
        _emulator.Memory[0x0207].ShouldBe(0x00);
        _emulator.Memory[0x0208].ShouldBe(0x60);
    }

    [Test]
    public void LoadingProgramToSpecifiedAddressSetsPC()
    {
        _emulator.LoadProgram("Test.prg", 0x0200);

        _emulator.CPU.Registers.PC.ShouldBe(0x0200);
    }

    [Test]
    public void ResetReloadsProgram()
    {
        _emulator.LoadProgram("Test.prg", 0x0200);
        _emulator.Memory[0x0200] = 0x00;

        _emulator.Reset();

        _emulator.Memory[0x0200].ShouldBe(0xA9);
    }

    [Test]
    public void CanPeekInstruction()
    {
        _emulator.LoadProgram("Test.prg");

        var op = _emulator.PeekInstruction();

        op.ShouldNotBeNull();
        op.Mnemonic.ShouldBe("LDA #$DE");
    }

    [Test]
    public void CanDisassembleInstruction()
    {
        _emulator.LoadProgram("Test.prg");

        var op = _emulator.Disassemble(0x8006);

        op.ShouldNotBeNull();
        op.Mnemonic.ShouldBe("DEC $00");
    }

    [Test]
    public void CanExecuteInstruction()
    {
        _emulator.LoadProgram("Test.prg");

        var op = _emulator.ExecuteInstruction();

        op.ShouldNotBeNull();
        op.Mnemonic.ShouldBe("LDA #$DE");

        _emulator.CPU.Registers.A.ShouldBe(0xDE);
        _emulator.CPU.Registers.PC.ShouldBe(0x8002);
    }
}
