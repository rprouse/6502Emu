global using NUnit.Framework;
global using Shouldly;

global using Mos6502Emu.Tests.Extensions;

// The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
#pragma warning disable CS8981
global using word = System.UInt16;
#pragma warning restore CS8981
