# 6502Emu

This is a simple 6502 emulator written in C#. It is not cycle accurate and only supports the documented 6502 opcodes, but it should
be good enough for most purposes.

## Instruction Set

The emulator supports the following instructions, [65C02 Instruction Set](65C02 Instructions.md).

## To-Do

- [x] Support documented 6502 instructions
- [x] Ability to step through a program
- [x] Disassemble a program
- [x] Set breakpoints
- [x] View registers
- [x] View memory
- [ ] Sort opcode helper methods and make them protected
- [ ] Switch default base address to 0x2000
- [ ] Support 65C02 extended instructions
- [ ] Interupt support
- [ ] Support for the 65C816 procesor
- [ ] Support multiple memory maps/hardware
