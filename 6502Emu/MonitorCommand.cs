using System.ComponentModel;
using Mos6502Emu.Core.Utilities;
using Spectre.Console.Cli;
using Monitor = Mos6502Emu.Monitor;

internal sealed class MonitorCommand : Command<MonitorCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "<program>")]
        [Description("Program to run.")]
        public string Program { get; set; } = string.Empty;

        [CommandArgument(1, "[baseAddress]")]
        [Description("Base address for the program.")]
        [DefaultValue("0x0200")]
        public string BaseAddress { get; set; } = string.Empty;

        [CommandOption("-c|--cpu")]
        [Description("CPU type to use.")]
        [DefaultValue(CpuType.W65C02S)]
        public CpuType CpuType { get; set; } = CpuType.W65C02S;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        word baseAddress = 0x0200;
        if (!string.IsNullOrWhiteSpace(settings.BaseAddress))
        {            
            if (settings.BaseAddress.TryParseHexWord(out var address))
                baseAddress = address;
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid base address[/]");
                return -1;
            }
        }

        Emulator emulator = new(settings.CpuType);
        Monitor monitor = new(emulator);
        monitor.Banner();

        return monitor.Run(settings.Program, baseAddress);
    }
}
