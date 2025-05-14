using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;
using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Tests.Processor.Opcodes;

public class Mos6502OpcodeAdressingModeTests
{
    private Mmu _mmu;
    private Registers _registers;
    private Mos6502OpcodeHandler _opcodeHandler;

    [SetUp]
    public void Setup()
    {
        // Initialize the CPU and memory
        _mmu = new Mmu();
        _mmu[0x8000] = 0x68;
        _mmu[0x8001] = 0x42;

        _registers = new Registers
        {
            A = 0x00,
            X = 0x00,
            Y = 0x00,
            P = 0b0010_0000,
            S = 0xFF,
            PC = 0x8000
        };

        _opcodeHandler = new Mos6502OpcodeHandler(_registers, _mmu);
    }

    [Test]
    public void TestImmediateAddressingMode()
    {
        byte result = _opcodeHandler.Immediate();
        result.Should().Be(0x68, because: "Immediate addressing mode should return the immediate value");
        _registers.PC.Should().Be(0x8001, because: "PC should be incremented by 1 after fetching the immediate value");
    }

    [Test]
    public void TestAbsoluteAddressingMode()
    {
        _mmu[0x4268] = 0x93; // Set the value at absolute address
        byte result = _opcodeHandler.Absolute();
        result.Should().Be(0x93, because: "Absolute addressing mode should return the value at absolute address");
        _registers.PC.Should().Be(0x8002, because: "PC should be incremented by 2 after fetching the absolute address");

        // Ensure the address is set correctly
        _opcodeHandler.Address.Should().Be(0x4268);
    }

    [Test]
    public void TestZeroPageAddressingMode()
    {
        _mmu[0x68] = 0x93; // Set the value at zero page address
        byte result = _opcodeHandler.ZeroPage();
        result.Should().Be(0x93, because: "Zero page addressing mode should return the value at zero page address");
        _registers.PC.Should().Be(0x8001, because: "PC should be incremented by 1 after fetching the zero page address");

        // Ensure the address is set correctly
        _opcodeHandler.Address.Should().Be(0x0068);
    }

    [TestCase((byte)0x68, (word)0x8068)]
    [TestCase((byte)0x98, (word)0x7F98)]
    public void TestRelativeAddressingMode(byte offset, word address)
    {
        _mmu[address] = 0x93; // Set the relative address
        _mmu[0x8000] = offset;
        byte result = _opcodeHandler.Relative();
        result.Should().Be(0x93, because: "Relative addressing mode should return the relative address");
        _registers.PC.Should().Be(0x8001, because: "PC should be incremented by 1 after fetching the relative address");

        // Ensure the address is set correctly  
        _opcodeHandler.Address.Should().Be(address);
    }

    [Test]
    public void TestIndirectAddressingMode()
    {
        _mmu[0x4268] = 0x00; // Set the low byte of the address
        _mmu[0x4269] = 0x40; // Set the high byte of the address
        _mmu[0x4000] = 0x93; // Set the value at the absolute indirect address
        _mmu[0x8000] = 0x68;
        byte result = _opcodeHandler.Indirect();
        result.Should().Be(0x93, because: "Absolute indirect addressing mode should return the value at absolute indirect address");
        _registers.PC.Should().Be(0x8002, because: "PC should be incremented by 2 after fetching the absolute indirect address");

        // Ensure the address is set correctly
        _opcodeHandler.Address.Should().Be(0x4000);
    }

    [Test]
    public void TestAbsoluteXAddressingMode()
    {
        _mmu[0x4278] = 0x93; // Set the value at absolute address plus X
        _registers.X = 0x10; // Set X register
        byte result = _opcodeHandler.AbsoluteX();
        result.Should().Be(0x93, because: "Absolute X addressing mode should return the value at absolute address + X");
        _registers.PC.Should().Be(0x8002, because: "PC should be incremented by 2 after fetching the absolute address");

        // Ensure the address is set correctly
        _opcodeHandler.Address.Should().Be(0x4278);
    }

    [Test]
    public void TestAbsoluteYAddressingMode()
    {
        _mmu[0x4278] = 0x93; // Set the value at absolute address plus Y
        _registers.Y = 0x10; // Set Y register
        byte result = _opcodeHandler.AbsoluteY();
        result.Should().Be(0x93, because: "Absolute Y addressing mode should return the value at absolute address + Y");
        _registers.PC.Should().Be(0x8002, because: "PC should be incremented by 2 after fetching the absolute address");

        // Ensure the address is set correctly
        _opcodeHandler.Address.Should().Be(0x4278);
    }

    [TestCase((byte)0x10, (word)0x0078)] // Positive offset
    [TestCase((byte)0xF0, (word)0x0058)] // Negative offset
    [TestCase((byte)0xB0, (word)0x0018)] // Wrapped offset
    public void TestZeroPageXAddressingMode(byte x, word address)
    {
        _mmu[(byte)address] = 0x93; // Set the value at zero page address plus X
        _registers.X = x; // Set X register
        byte result = _opcodeHandler.ZeroPageX();
        result.Should().Be(0x93, because: "Zero page X addressing mode should return the value at zero page address + X");
        _registers.PC.Should().Be(0x8001, because: "PC should be incremented by 1 after fetching the zero page address");

        // Ensure the address is set correctly
        _opcodeHandler.Address.Should().Be(address);
    }

    [TestCase((byte)0x10, (word)0x0078)] // Positive offset
    [TestCase((byte)0xF0, (word)0x0058)] // Negative offset
    [TestCase((byte)0xB0, (word)0x0018)] // Wrapped offset
    public void TestZeroPageYAddressingMode(byte y, word address)
    {
        _mmu[(byte)address] = 0x93; // Set the value at zero page address plus Y
        _registers.Y = y; // Set Y register
        byte result = _opcodeHandler.ZeroPageY();
        result.Should().Be(0x93, because: "Zero page Y addressing mode should return the value at zero page address + Y");
        _registers.PC.Should().Be(0x8001, because: "PC should be incremented by 1 after fetching the zero page address");

        // Ensure the address is set correctly
        _opcodeHandler.Address.Should().Be(address);
    }

    [Test]
    public void TestIndirectXAddressingMode()
    {
        _mmu[0x0078] = 0x00; // Set the low byte of the address
        _mmu[0x0079] = 0x40; // Set the high byte of the address
        _mmu[0x4000] = 0x93; // Set the value at the indirect address plus X
        _registers.X = 0x10; // Set X register
        byte result = _opcodeHandler.ZeroPageIndirectX();
        result.Should().Be(0x93, because: "Indirect X addressing mode should return the value at indirect address + X");
        _registers.PC.Should().Be(0x8001, because: "PC should be incremented by 1 after fetching the indirect address");

        // Ensure the address is set correctly
        _opcodeHandler.Address.Should().Be(0x4000);
    }

    [Test]
    public void TestIndirectYAddressingMode()
    {
        _mmu[0x68] = 0x00; // Set the low byte of the address
        _mmu[0x69] = 0x40; // Set the high byte of the address
        _mmu[0x4010] = 0x93; // Set the value at the indirect address plus Y
        _registers.Y = 0x10; // Set Y register
        byte result = _opcodeHandler.ZeroPageIndirectY();
        result.Should().Be(0x93, because: "Indirect Y addressing mode should return the value at indirect address + Y");
        _registers.PC.Should().Be(0x8001, because: "PC should be incremented by 1 after fetching the indirect address");

        // Ensure the address is set correctly
        _opcodeHandler.Address.Should().Be(0x4010);
    }
}
