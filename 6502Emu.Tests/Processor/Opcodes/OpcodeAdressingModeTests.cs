using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;
using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Tests.Processor.Opcodes;

public class OpcodeAdressingModeTests
{
    private Mmu mmu;
    private Registers registers;
    private OpcodeHandler opcodeHandler;

    [SetUp]
    public void Setup()
    {
        // Initialize the CPU and memory
        mmu = new Mmu();
        mmu[0x0200] = 0x68;
        mmu[0x0201] = 0x42;

        registers = new Registers
        {
            A = 0x00,
            X = 0x00,
            Y = 0x00,
            P = 0b0010_0000,
            S = 0xFF,
            PC = 0x0200
        };

        opcodeHandler = new OpcodeHandler(registers, mmu);
    }

    [Test]
    public void TestImmediateAddressingMode()
    {
        byte result = opcodeHandler.Immediate();
        result.Should().Be(0x68, because: "Immediate addressing mode should return the immediate value");
        registers.PC.Should().Be(0x0201, because: "PC should be incremented by 1 after fetching the immediate value");
    }

    [Test]
    public void TestAbsoluteAddressingMode()
    {
        mmu[0x4268] = 0x93; // Set the value at absolute address
        byte result = opcodeHandler.Absolute();
        result.Should().Be(0x93, because: "Absolute addressing mode should return the value at absolute address");
        registers.PC.Should().Be(0x0202, because: "PC should be incremented by 2 after fetching the absolute address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x4268);
    }

    [Test]
    public void TestZeroPageAddressingMode()
    {
        mmu[0x68] = 0x93; // Set the value at zero page address
        byte result = opcodeHandler.ZeroPage();
        result.Should().Be(0x93, because: "Zero page addressing mode should return the value at zero page address");
        registers.PC.Should().Be(0x0201, because: "PC should be incremented by 1 after fetching the zero page address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x0068);
    }

    [Test]
    public void TestRelativeAddressingMode()
    {
        mmu[0x0268] = 0x93; // Set the relative address
        byte result = opcodeHandler.Relative();
        result.Should().Be(0x93, because: "Relative addressing mode should return the relative address");
        registers.PC.Should().Be(0x0201, because: "PC should be incremented by 1 after fetching the relative address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x0268);
    }

    [Test]
    public void TestAbsoluteIndirectAddressingMode()
    {
        mmu[0x4268] = 0x00; // Set the low byte of the address
        mmu[0x4269] = 0x80; // Set the high byte of the address
        mmu[0x8000] = 0x93; // Set the value at the absolute indirect address
        byte result = opcodeHandler.Indirect();
        result.Should().Be(0x93, because: "Absolute indirect addressing mode should return the value at absolute indirect address");
        registers.PC.Should().Be(0x0202, because: "PC should be incremented by 2 after fetching the absolute indirect address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x8000);
    }

    [Test]
    public void TestAbsoluteXAddressingMode()
    {
        mmu[0x4278] = 0x93; // Set the value at absolute address plus X
        registers.X = 0x10; // Set X register
        byte result = opcodeHandler.AbsoluteX();
        result.Should().Be(0x93, because: "Absolute X addressing mode should return the value at absolute address + X");
        registers.PC.Should().Be(0x0202, because: "PC should be incremented by 2 after fetching the absolute address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x4278);
    }

    [Test]
    public void TestAbsoluteYAddressingMode()
    {
        mmu[0x4278] = 0x93; // Set the value at absolute address plus Y
        registers.Y = 0x10; // Set Y register
        byte result = opcodeHandler.AbsoluteY();
        result.Should().Be(0x93, because: "Absolute Y addressing mode should return the value at absolute address + Y");
        registers.PC.Should().Be(0x0202, because: "PC should be incremented by 2 after fetching the absolute address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x4278);
    }

    [Test]
    public void TestZeroPageXAddressingMode()
    {
        mmu[0x78] = 0x93; // Set the value at zero page address plus X
        registers.X = 0x10; // Set X register
        byte result = opcodeHandler.ZeroPageX();
        result.Should().Be(0x93, because: "Zero page X addressing mode should return the value at zero page address + X");
        registers.PC.Should().Be(0x0201, because: "PC should be incremented by 1 after fetching the zero page address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x0078);
    }

    [Test]
    public void TestZeroPageXAddressingModeWrapped()
    {
        mmu[0x18] = 0x93; // Set the value at zero page address plus X
        registers.X = 0xB0; // Set X register
        byte result = opcodeHandler.ZeroPageX();
        result.Should().Be(0x93, because: "Zero page X addressing mode should return the value at zero page address + X");
        registers.PC.Should().Be(0x0201, because: "PC should be incremented by 1 after fetching the zero page address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x0018);
    }

    [Test]
    public void TestZeroPageYAddressingMode()
    {
        mmu[0x78] = 0x93; // Set the value at zero page address plus Y
        registers.Y = 0x10; // Set Y register
        byte result = opcodeHandler.ZeroPageY();
        result.Should().Be(0x93, because: "Zero page Y addressing mode should return the value at zero page address + Y");
        registers.PC.Should().Be(0x0201, because: "PC should be incremented by 1 after fetching the zero page address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x0078);
    }

    [Test]
    public void TestZeroPageYAddressingModeWrapped()
    {
        mmu[0x18] = 0x93; // Set the value at zero page address plus Y
        registers.Y = 0xB0; // Set Y register
        byte result = opcodeHandler.ZeroPageY();
        result.Should().Be(0x93, because: "Zero page Y addressing mode should return the value at zero page address + Y");
        registers.PC.Should().Be(0x0201, because: "PC should be incremented by 1 after fetching the zero page address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x0018);
    }

    [Test]
    public void TestIndirectXAddressingMode()
    {
        mmu[0x78] = 0x00; // Set the low byte of the address
        mmu[0x79] = 0x80; // Set the high byte of the address
        mmu[0x8000] = 0x93; // Set the value at the indirect address plus X
        registers.X = 0x10; // Set X register
        byte result = opcodeHandler.ZeroPageIndirectX();
        result.Should().Be(0x93, because: "Indirect X addressing mode should return the value at indirect address + X");
        registers.PC.Should().Be(0x0201, because: "PC should be incremented by 1 after fetching the indirect address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x8000);
    }

    [Test]
    public void TestIndirectXAddressingModeWrapped()
    {
        mmu[0x18] = 0x00; // Set the low byte of the address
        mmu[0x19] = 0x80; // Set the high byte of the address
        mmu[0x8000] = 0x93; // Set the value at the indirect address plus X
        registers.X = 0xB0; // Set X register
        byte result = opcodeHandler.ZeroPageIndirectX();
        result.Should().Be(0x93, because: "Indirect X addressing mode should return the value at indirect address + X");
        registers.PC.Should().Be(0x0201, because: "PC should be incremented by 1 after fetching the indirect address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x8000);
    }

    [Test]
    public void TestIndirectYAddressingMode()
    {
        mmu[0x68] = 0x00; // Set the low byte of the address
        mmu[0x69] = 0x80; // Set the high byte of the address
        mmu[0x8010] = 0x93; // Set the value at the indirect address plus Y
        registers.Y = 0x10; // Set Y register
        byte result = opcodeHandler.ZeroPageIndirectY();
        result.Should().Be(0x93, because: "Indirect Y addressing mode should return the value at indirect address + Y");
        registers.PC.Should().Be(0x0201, because: "PC should be incremented by 1 after fetching the indirect address");

        // Ensure the address is set correctly
        opcodeHandler.Address.Should().Be(0x8010);
    }
}
