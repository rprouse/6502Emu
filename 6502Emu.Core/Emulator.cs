using System.Diagnostics.CodeAnalysis;
using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;
using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Core;

public class Emulator
{
    private string? _filename;
    private word? _baseAddress;

    public Mmu Memory { get; private set; }

    public ICpu CPU { get; private set; }

    public CpuType CpuType { get; }

    public bool WarmBoot { get; set; }

    public Emulator(CpuType cpuType)
    {
        CpuType = cpuType;
        Reset();
    }

    public bool LoadProgram(string filename, word baseAddress = 0x0200)
    {
        _filename = filename;
        _baseAddress = baseAddress;
        bool result = Memory.LoadProgram(filename, baseAddress);
        if (result) CPU.Registers.PC = baseAddress;
        return result;
    }

    [MemberNotNull(nameof(Memory), nameof(CPU))]
    public void Reset()
    {
        WarmBoot = false;
        Memory = new Mmu();

        CPU = CpuType.CreateCpu(Memory);
        if (_filename != null && _baseAddress.HasValue)
            Memory.LoadProgram(_filename, _baseAddress.Value);

    }

    public Opcode ExecuteInstruction() => CPU.ExecuteInstruction();

    public Opcode PeekInstruction() => CPU.PeekInstruction(CPU.Registers.PC);

    public Opcode Disassemble(word addr) => CPU.PeekInstruction(addr);
}
