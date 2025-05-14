using Mos6502Emu.Core.Processor.Opcodes;

namespace Mos6502Emu.Core.Processor
{
    public interface ICpu
    {
        /// <summary>
        /// The CPU registers.
        /// </summary>
        Registers Registers { get; }

        /// <summary>
        /// Gets the current instruction at the specified address without executing it.
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        Opcode PeekInstruction(ushort addr);

        /// <summary>
        /// Executes the current instruction and returns the opcode.
        /// </summary>
        /// <returns>The executed opcode.</returns>
        Opcode ExecuteInstruction();
    }
}
