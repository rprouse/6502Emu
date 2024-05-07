using System.Diagnostics.CodeAnalysis;
using Mos6502Emu.Core.Memory;
using Mos6502Emu.Core.Processor;

namespace Mos6502Emu.Core;
public class Emulator
{
    private string? _filename;
    private word? _baseAddress;

    public Mmu Memory { get; private set; }

    public Cpu CPU { get; private set; }

    public Emulator()
    {
        Reset();
    }

    public bool LoadProgram(string filename, word baseAddress = 0x0100)
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
        Memory = new Mmu();

        CPU = new Cpu(Memory);
        if (_filename != null && _baseAddress.HasValue)
            Memory.LoadProgram(_filename, _baseAddress.Value);

    }
}
