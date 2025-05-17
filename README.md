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
- [x] Sort opcode helper methods and make them protected
- [x] Switch default base address to 0x0200
- [x] Support 65C02 extended instructions
- [ ] Interrupt support
- [ ] Possible support for the 65C816 processor
- [ ] Support multiple memory maps/hardware
- [ ] Add Load/Save commands
- [ ] CI/CD pipeline
- [ ] Implement STP and WAI instructions
