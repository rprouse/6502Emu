using Mos6502Emu.Core.Utilities;
using Monitor = Mos6502Emu.Monitor;

Emulator emulator = new(CpuType.MOS6502);
Monitor monitor = new(emulator);
monitor.Banner();

if (args.Length < 1 || args.Length > 2)
{
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[blue]Usage:[/] [silver]6502Emu <program.prg> [[baseAddress]][/]");
    AnsiConsole.MarkupLine("[blue]Example:[/] [silver]6502Emu hello.prg 0x4000[/]");
    AnsiConsole.MarkupLine("[blue]Default base address is 0x8000. Use HEX.[/]");
    return -1;
}

word baseAddress = 0x8000;
if (args.Length == 2)
{
    if (args[1].StartsWith("0x")) args[1] =
            args[1].Substring(2);

    if (args[1].TryParseHexWord(out var address))
        baseAddress = address;
    else
        AnsiConsole.MarkupLine("[red]Invalid base address[/]");
}

return monitor.Run(args[0], baseAddress);
