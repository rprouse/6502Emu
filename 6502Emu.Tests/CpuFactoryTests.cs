using Mos6502Emu.Core;
using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;

namespace Mos6502Emu.Tests;

[TestFixture]
public class CpuFactoryTests
{
    private Mmu _mmu;

    [SetUp]
    public void Setup()
    {
        _mmu = new Mmu();
    }

    [Test]
    public void CreateCpu_WithMOS6502Type_ReturnsMos6502CpuInstance()
    {
        // Arrange
        var cpuType = CpuType.MOS6502;

        // Act
        var cpu = cpuType.CreateCpu(_mmu);

        // Assert
        cpu.ShouldNotBeNull();
        cpu.ShouldBeOfType<Mos6502Cpu>();
    }

    [Test]
    public void CreateCpu_WithUnsupportedType_ThrowsNotSupportedException()
    {
        // Arrange
        var cpuType = (CpuType)999; // Invalid CPU type

        // Act & Assert
        var action = () => cpuType.CreateCpu(_mmu);
        action.ShouldThrow<NotSupportedException>()
            .Message.ShouldBe($"CPU type {cpuType} is not supported.");
    }

    [Test]
    public void CreateCpu_WithWD65C02Type_ThrowsNotSupportedException()
    {
        // Arrange
        var cpuType = CpuType.WD65C02;

        // Act & Assert
        var action = () => cpuType.CreateCpu(_mmu);
        action.ShouldThrow<NotSupportedException>()
            .Message.ShouldBe($"CPU type {cpuType} is not supported.");
    }
}
