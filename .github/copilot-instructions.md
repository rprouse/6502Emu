# 6502 CPU Emulator - GitHub Copilot Guidelines

This document outlines the architecture, coding standards, and design principles for the 6502 CPU emulator project.

## Project Overview

The 6502Emu project is a command-line emulator for the 6502/65C02 CPU written in C#. The project aims to provide an accurate emulation of the 6502 processor, including its instruction set, memory management, and I/O capabilities.

## Solution Architecture

The solution is organized into multiple projects:

1. **6502Emu.Core**: The core library containing the emulation engine
   - `Processor/`: CPU and register implementations
   - `Memory/`: Memory management (MMU and memory blocks)

2. **6502Emu**: The command-line interface for the emulator
   - Uses Spectre.Console for console UI rendering

3. **6502Emu.Tests**: Unit tests for the emulator using NUnit
   - Includes test cases for CPU, memory, and overall emulation

4. **ParseOpcodes**: A utility for parsing 6502 instruction data

## Code Style and Patterns

### General Coding Standards

1. **C# Coding Conventions**: Follow standard C# coding conventions including PascalCase for public members and camelCase for private members with underscore prefix.

2. **Nullable Reference Types**: The project uses nullable reference types (`<Nullable>enable</Nullable>`). Use appropriate annotations (`?`, `!`, `[MemberNotNull]`, etc.) as needed.

3. **XML Documentation**: Public APIs should include XML documentation comments.

4. **Immutability**: Prefer immutable types where possible.

5. **Exception Handling**: Use appropriate exception handling for exceptional cases.

### Class Design Patterns

1. **Memory Subsystem**:
   - `Mmu` acts as the central memory management unit
   - `MemoryBlock` represents a contiguous block of memory at a specified address range
   - Memory access is abstracted through indexers

2. **CPU Implementation**:
   - `Cpu` class manages the processor state and execution
   - `Registers` class encapsulates the 6502 registers (A, X, Y, S, P, PC)
   - `Flag` enum defines the processor status flags

3. **Emulator**:
   - `Emulator` class ties everything together and provides the main API

## Dependencies

1. **Framework**: .NET 8.0

2. **NuGet Packages**:
   - **Spectre.Console**: Used for rich console UI/rendering
   - **NUnit**: Used for unit testing
   - **FluentAssertions.Json**: Used for test assertions

## Implementation Guidelines

### CPU Emulation

1. Implement the 6502 instruction set accurately according to reference documentation.
2. Each instruction should have a matching method with appropriate cycles counted.
3. Address modes should be implemented as separate methods.
4. Status flags should be set correctly after each operation.

### Memory Management

1. Memory is accessed through the MMU, which maps addresses to appropriate memory blocks.
2. Memory mapped I/O should be implemented through specialized memory blocks.
3. ROM and RAM should be properly differentiated.

### Console Interface

1. Use Spectre.Console features for a rich command-line experience.
2. Implement a monitor interface for debugging and interaction with the emulated system.
3. Provide clear error messages and user guidance.

## Best Practices

1. **Testing**: Maintain comprehensive test coverage, especially for CPU instructions.
2. **Performance**: Consider performance optimizations where appropriate, but prioritize correctness.
3. **Documentation**: Keep code well-documented, especially complex emulation logic.
4. **Modular Design**: Keep components loosely coupled with clear interfaces.

## References

- [6502 Opcodes](http://6502.org/tutorials/6502opcodes.html)
- [6502 Instruction Set](https://www.masswerk.at/6502/6502_instruction_set.html)
- [6502.org](http://www.6502.org/) - Comprehensive 6502 Resources
