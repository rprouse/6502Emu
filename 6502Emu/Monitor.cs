// See https://aka.ms/new-console-template for more information
using System.Reflection;

namespace Mos6502Emu;

public class Monitor
{
    public void Banner()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version;
        var fontDir = Path.Combine(Path.GetDirectoryName(assembly.Location) ?? ".", "font", "ANSI Shadow.flf");
        FigletFont font = FigletFont.Load(fontDir);

        AnsiConsole.Write(
            new FigletText(font, "MOS 6502")
                .LeftJustified()
                .Color(Color.Blue));
        AnsiConsole.MarkupLine($"[yellow]8-Bit Retro 6502 Emulator by Rob Prouse v{version?.ToString(3)}[/]");
        //AnsiConsole.MarkupLine($"[blue]Running {_emulator.OperatingSystem.Name}[/]");
    }
}