using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;
using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Tests.Processor.Opcodes;

public class W65C02SOpcodeAddressingModeTests
{
    private Mmu _mmu;
    private Registers _registers;
    private W65C02SOpcodeHandler _opcodeHandler;

    [SetUp]
    public void Setup()
    {
        // Initialize the CPU and memory
        _mmu = new Mmu();
        _mmu[0x0200] = 0x68;
        _mmu[0x0201] = 0x42;

        _registers = new Registers
        {
            A = 0x00,
            X = 0x00,
            Y = 0x00,
            P = 0b0010_0000,
            S = 0xFF,
            PC = 0x0200
        };

        _opcodeHandler = new W65C02SOpcodeHandler(_registers, _mmu);
    }

    [Test]
    public void TestZeroPageIndirectAddressingMode()
    {
        _mmu[0x0068] = 0x00; // Set the low byte of the address
        _mmu[0x0069] = 0x40; // Set the high byte of the address
        _mmu[0x4000] = 0x93; // Set the value at the indirect address plus X
        byte result = _opcodeHandler.ZeroPageIndirect();
        result.ShouldBe(0x93, "Zero Page Indirect addressing mode should return the value at indirect address");
        _registers.PC.ShouldBe(0x0201, "PC should be incremented by 1 after fetching the indirect address");

        // Ensure the address is set correctly
        _opcodeHandler.Address.ShouldBe(0x4000);
    }

    [Test]
    public void TestAbsoluteIndexedIndirectAddressingMode()
    {
        _mmu[0x4278] = 0x00; // Set the low byte of the address
        _mmu[0x4279] = 0x40; // Set the high byte of the address
        _mmu[0x4000] = 0x93; // Set the value at the absolute indirect address plus X
        _registers.X = 0x10; // Set X register
        byte result = _opcodeHandler.AbsoluteIndexedIndirect();
        result.ShouldBe(0x93, "Absolute Indexed Indirect addressing mode should return the value at absolute indirect address + X");
        _registers.PC.ShouldBe(0x0202, "PC should be incremented by 2 after fetching the absolute indirect address");
        // Ensure the address is set correctly
        _opcodeHandler.Address.ShouldBe(0x4000);
    }
}
