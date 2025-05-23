namespace Mos6502Emu.Core.Memory;

/// <summary>
/// Controls access to all memory on the device
/// </summary>
public class Mmu
{
    private readonly MemoryBlock _ram;
    private readonly MemoryBlock[] _memoryBlocks;

    public Mmu()
    {
        // TODO: Add support for Zero Page, ROM, RAM, IO, etc.
        _ram = new MemoryBlock(0x0000, 0xFFFF);
        _memoryBlocks = new[] { _ram };
    }

    /// <summary>
    /// Loads a program from a file to 0x0200
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public bool LoadProgram(string filename, word baseAddress = 0x0200)
    {
        if (!File.Exists(filename)) return false;
        byte[] data = File.ReadAllBytes(filename);
        _ram.Copy(data, baseAddress);
        return true;
    }

    public byte this[int address]
    {
        get => Read(address);
        set => Write(address, value);
    }

    private byte Read(int address)
    {
        var memory = _memoryBlocks.FirstOrDefault(m => m.HandlesAddress(address));
        if (memory != null) return memory[address];

        throw new IndexOutOfRangeException($"Address 0x{address:X2} not mapped");
    }

    private void Write(int address, byte data)
    {
        var memory = _memoryBlocks.FirstOrDefault(m => m.HandlesAddress(address));
        if (memory != null) memory[address] = data;
        else throw new IndexOutOfRangeException($"Address 0x{address:X2} not mapped");
    }
}

