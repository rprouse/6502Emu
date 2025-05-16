namespace Mos6502Emu.Core.Processor.Opcodes;

public interface IOpcodeHandler
{
    /// <summary>
    /// The absolute address of the last instruction executed
    /// </summary>
    word Address { get; }

    /// <summary>
    /// Fetches the next opcode from memory and increments the program counter
    /// </summary>
    Opcode FetchInstruction();

    /// <summary>
    /// Gets the opcode for a given hex value
    /// </summary>
    Opcode GetOpcode(byte hex);

    /// <summary>
    /// Peeks at the opcode at a given address without modifying the program counter
    /// </summary>
    Opcode PeekInstruction(word addr);
}
