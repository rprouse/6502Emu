# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build, Run, and Test

The solution is .NET 10.0; commands work from the repo root.

```bash
dotnet build 6502Emu.sln
dotnet test 6502Emu.Tests/6502Emu.Tests.csproj
```

Run the emulator (CLI usage — the `<program>` is a raw binary loaded into RAM):

```bash
dotnet run --project 6502Emu -- <program> [baseAddress] [-c|--cpu MOS6502|W65C02S]
```

- `baseAddress` is hex (e.g. `0x8000`) and defaults to `0x8000`. The PC is set to this address after load.
- `--cpu` defaults to `W65C02S`.

Once running, the monitor takes single-letter commands: `s`tep, `r`un (to breakpoint), `m`emory `[hex]`, `reg`, `d`isassemble `[hex]`, `b`reakpoints (sub-menu), `reset`, `h`elp, `q`uit. Pressing Enter alone repeats the last command.

Run a single NUnit test (use the fully-qualified test name shape `TestOpcode(0xNN, ...)` matched by `--filter`):

```bash
dotnet test 6502Emu.Tests/6502Emu.Tests.csproj --filter "FullyQualifiedName~Mos6502OpcodeHandlerTests"
dotnet test 6502Emu.Tests/6502Emu.Tests.csproj --filter "Name~TestOpcode(0x69"
```

## Architecture

Three projects, all under the `Mos6502Emu*` root namespace (note: `MOS` not `6502` — the namespace was kept after the file rename):

- **`6502Emu.Core`** — emulation library. The execution stack from the top down: `Emulator` owns an `Mmu` and an `ICpu` produced by `CpuFactory.CreateCpu(CpuType)`. Each call to `Emulator.ExecuteInstruction()` delegates to `ICpu.ExecuteInstruction()`, which fetches an `Opcode` from its `IOpcodeHandler` and invokes the lambda stored in `opcode.Execute`.
- **`6502Emu`** — CLI. `Program.cs` is two lines that boot a `Spectre.Console.Cli` `CommandApp<MonitorCommand>`. `MonitorCommand` constructs `Emulator` → `Monitor` → `monitor.Run(...)`. `Monitor.cs` is the interactive REPL.
- **`6502Emu.Tests`** — NUnit + Shouldly. The bulk of coverage runs through `OpcodeTestBase` against Tom Harte's single-step JSON test vectors (see below).

### Opcode handler split (the central pattern)

Each CPU's opcode handler is a `partial class` split across four files. When adding or modifying instructions, expect to touch multiple files:

| File | Role |
|---|---|
| `Mos6502OpcodeHandler.cs` | State (`_reg`, `_mmu`, `_opcodes` dictionary, `_address` carry-between-ticks), the `FetchInstruction` / `PeekInstruction` / `GetOpcode` API, plus the per-mnemonic helpers (`LDA`, `ADC`, `Branch`, etc.) |
| `Mos6502OpcodeHandler.AddressingModes.cs` | One method per addressing mode (`Immediate`, `ZeroPage`, `AbsoluteX`, `ZeroPageIndirectY`, …). Each returns the operand byte and side-effects `_address` for store-style instructions. |
| `Mos6502OpcodeHandler.Initialize.cs` | `InitializeOpcodes()` — registers `Opcode` metadata (mnemonic, addressing mode string, hex byte, length, description) into `_opcodes` via `Add(...)`. |
| `Mos6502OpcodeHandler.Methods.cs` | `InitializeMethods()` — wires each opcode's `Execute` lambda, e.g. `_opcodes[0x69].Execute = () => ADC(Immediate());`. |

`W65C02SOpcodeHandler` extends `Mos6502OpcodeHandler` (same four-file split) and overrides `InitializeOpcodes` / `InitializeMethods` to call `base` first, then register the 65C02-specific instructions (`BRA`, `STZ`, `TSB`/`TRB`, `RMB`/`SMB`, `BBR`/`BBS`, `(zp)` addressing, etc.). `BRK`, `ADC_Decimal`, and `SBC_Decimal` are `protected virtual` on the base specifically so the 65C02 can override BCD semantics and the post-BRK decimal-flag clear.

### CPU subclassing

`Mos6502Cpu.CreateOpcodeHandler(reg, mmu)` is `protected virtual`; `W65C02SCpu` overrides it to return `W65C02SOpcodeHandler`. The constructor sets defaults `A=X=Y=0`, `S=0xFF`, `P=0b0010_0000`, `PC=0xFFFC`. Reset-vector loading from `[0xFFFC]/[0xFFFD]` is **not** implemented — `Emulator.LoadProgram` instead just sets `PC = baseAddress`.

### Memory model

`Mmu` currently exposes a single `MemoryBlock(0x0000, 0xFFFF)` (full 64 KiB RAM). Address dispatch goes through `_memoryBlocks.FirstOrDefault(m => m.HandlesAddress(address))`, so the framework is ready for ROM/IO/zero-page split blocks but only one is wired up today. **Unmapped reads/writes throw `IndexOutOfRangeException`** — don't add silent-fallback behavior. The stack lives at `0x0100 + S` (hard-coded across handlers).

### `Opcode.Execute` and disassembly

`Opcode` carries both runtime behavior (`Action? Execute`) and disassembly state (substitution slots `_n`, `_d`, `_nn` filled by `SetSubstitutions(mmu, addr)`). `PeekInstruction` calls `SetSubstitutions` so that `Mnemonic` renders operands like `LDA $80,X` for the monitor's disassembly view — the same `Opcode` object is reused for both execution and display. New addressing modes need a switch arm in **both** `SetSubstitutions` and the `Mnemonic` getter.

### The `word` type alias

Each project's `Usings.cs` declares `global using word = System.UInt16` (with `#pragma warning disable CS8981` to silence the lowercase-type-name warning). Use `word` for any 16-bit address quantity throughout the codebase — it's the dominant convention.

## Testing — Tom Harte single-step vectors

`6502Emu.Tests/OpcodeData/Mos6502/*.json` and `OpcodeData/W65C02S/*.json` are vendored from https://github.com/SingleStepTests/65x02 (MIT, by Thomas Harte), reduced from 10,000 cases per opcode to ~100, documented opcodes only. Each JSON file is named for the opcode hex (`69.json` = ADC immediate). Each case has `initial` / `final` snapshots of registers + RAM cells; `OpcodeTestBase.TestOpcode` sets state, executes one instruction, and asserts every register plus the listed RAM cells.

`Mos6502OpcodeHandlerTests` runs **10** cases per opcode; `W65C02SOpcodeHandlerTests` runs **4**. Bumping these constants raises confidence at the cost of test runtime. There is also a `TestOpcodeIsImplemented` pass that just checks every opcode resolves to a non-null `Execute` lambda — useful when adding new opcode metadata.

When adding a new opcode, make sure the corresponding `<hex>.json` is in the test-data directory **and** is listed in `6502Emu.Tests.csproj` with `<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>` (every JSON file is enumerated explicitly in the csproj).

## Conventions

- **Style** (from `.github/copilot-instructions.md`): PascalCase public members, camelCase with `_` prefix for private fields, nullable reference types enabled, XML doc on public APIs.
- **Code analysis suppressions** are centralized in `GlobalSuppressions.cs`. Two existing rules are suppressed project-wide (`S112` and `S6602`); follow the same pattern rather than scattering `#pragma`s.
- The README To-Do calls out two known refactors not yet done: "sort opcode helper methods and make them protected" and "switch default base address to 0x2000". Don't rely on either.
- Whenever making changes to the application, increase the version number in `6502Emu.csproj` (e.g. `0.1.0` → `0.2.0`) to keep track of iterations and ensure the latest test assembly is built. Use semantic versioning principles: increment the patch version for bug fixes, the minor version for new features, and the major version for breaking changes.

## Git on Windows

When running on Windows, always use the PowerShell tool (not Bash) for `git push`. Other git commands work fine via either shell, but pushes specifically must go through PowerShell.

## Reference

The full 65C02 instruction list with bytes/mnemonics/descriptions lives in `65C02 Instructions.md` at the repo root — useful when adding or verifying opcode metadata.
