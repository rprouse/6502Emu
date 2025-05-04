// See https://aka.ms/new-console-template for more information
using System;
using Mos6502Emu;
using Monitor = Mos6502Emu.Monitor;

Emulator emulator = new();
Monitor monitor = new(emulator);
monitor.Banner();
