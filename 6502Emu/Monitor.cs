// See https://aka.ms/new-console-template for more information
using System.Reflection;
using Mos6502Emu.Core.Processor.Opcodes;
using Mos6502Emu.Core.Utilities;

namespace Mos6502Emu;

public class Monitor
{
    readonly Emulator _emulator;
    readonly SortedSet<word> _breakpoints = new SortedSet<word>();

    word? _lastMemAddr;
    word? _lastDisAddr;

    public Monitor(Emulator emulator)
    {
        _emulator = emulator;
    }

    public int Run(string filename, word baseAddress = 0x0200)
    {
        AnsiConsole.MarkupLine($"[Blue]Loading {filename}[/]");

        if (!_emulator.LoadProgram(filename, baseAddress))
        {
            AnsiConsole.MarkupLine("[Red]File not found[/]");
            return -1;
        }

        string lastCommand = "s";

        while (true)
        {
            AnsiConsole.Markup("> ");
            string? command = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(command)) command = lastCommand;
            string[] parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            lastCommand = parts[0];

            switch (parts[0])
            {
                case "s":   // Step
                    if (Step())
                        lastCommand = "q";
                    break;
                case "r":   // Run to Breakpoint
                    if (Run())
                        lastCommand = "q";
                    break;
                case "m":   // Memory
                    if (parts.Length == 2 && parts[1].TryParseHexWord(out word memAddr))
                        _lastMemAddr = memAddr;

                    ViewMemory(_lastMemAddr ?? 0x100);
                    break;
                case "reg": // Registers
                    ViewRegisters();
                    break;
                case "d":   // Disassemble
                    if (parts.Length == 2 && parts[1].TryParseHexWord(out word disAddr))
                        _lastDisAddr = disAddr;

                    ViewDisassembly(_lastDisAddr ?? _emulator.CPU.Registers.PC);
                    break;
                case "b":   // Breakpoints
                    ManageBreakpoints();
                    break;
                case "reset":   // Reset
                    _emulator.Reset();
                    break;
                case "h":   // Help
                    ViewHelp();
                    break;
                case "q":   // Quit
                    return 0;
                default:
                    AnsiConsole.MarkupLine("[red]Unknown command[/]");
                    break;
            }
        }
    }

    public void Banner()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version;
        var fontDir = Path.Combine(Path.GetDirectoryName(assembly.Location) ?? ".", "font", "ANSI Shadow.flf");
        FigletFont font = FigletFont.Load(fontDir);

        string cpu = _emulator.CpuType switch
        {
            CpuType.W65C02S => "WDC 65C02",
            CpuType.MOS6502 => "MOS 6502",
            _ => "6502"
        };

        AnsiConsole.Write(
            new FigletText(font, cpu)
                .LeftJustified()
                .Color(Color.Blue));

        AnsiConsole.MarkupLine($"[yellow]8-Bit Retro 6502 Emulator by Rob Prouse v{version?.ToString(3)}[/]");
        //AnsiConsole.MarkupLine($"[blue]Running {_emulator.OperatingSystem.Name}[/]");
    }

    bool IsTopLevelReturn(Opcode? opcode)
    {
        if (_emulator.WarmBoot)
        {
            _emulator.WarmBoot = false;
            return true;
        }

        if (opcode?.Mnemonic == "RTS" && _emulator.CPU.Registers.S == 0xFF)
            return true;

        return false;
    }

    void ViewOpcode(word addr, Opcode? opcode)
    {
        AnsiConsole.Markup($"[blue]{addr:X4}[/]");

        if (opcode == null)
        {
            AnsiConsole.MarkupLine($" [aqua]{_emulator.Memory[addr]:X2}[/]");
            return;
        }

        if (addr == _emulator.CPU.Registers.PC && IsBreakpointSet(addr, opcode))
            AnsiConsole.Markup($"[red]*< [/]");
        else if (addr == _emulator.CPU.Registers.PC)
            AnsiConsole.Markup($"[red]<  [/]");
        else if (IsBreakpointSet(addr, opcode))
            AnsiConsole.Markup($"[red]*  [/]");
        else
            AnsiConsole.Markup($"   ");

        for (int i = 0; i < 4; i++)
        {
            if (i < opcode.Length)
                AnsiConsole.Markup($"[aqua]{_emulator.Memory[addr + i]:X2}[/] ");
            else
                AnsiConsole.Markup($"   ");
        }

        // Longest opcode with substitution is 15 characters
        AnsiConsole.Markup($"[silver]{opcode.Mnemonic.PadRight(15)}[/]");

        if (opcode.Mnemonic != "NOP")
            AnsiConsole.MarkupLine($"\t[green]; {opcode.Description}[/]");
        else
            AnsiConsole.WriteLine();
    }

    bool Step()
    {
        _lastMemAddr = null;
        _lastDisAddr = null;
        word addr = _emulator.CPU.Registers.PC;
        Opcode? opcode = _emulator.ExecuteInstruction();

        ViewOpcode(addr, opcode); // View the opcode we just executed
        opcode = _emulator.PeekInstruction();
        ViewOpcode(_emulator.CPU.Registers.PC, opcode); // View the next opcode
        AnsiConsole.WriteLine();
        ViewRegisters();
        return IsTopLevelReturn(opcode);
    }

    bool Run()
    {
        _lastMemAddr = null;
        _lastDisAddr = null;
        Opcode? opcode = null;
        word addr;
        do
        {
            addr = _emulator.CPU.Registers.PC;
            opcode = _emulator.ExecuteInstruction();
        }
        while (opcode != null && !IsBreakpointSet(_emulator.CPU.Registers.PC, opcode) && !IsTopLevelReturn(_emulator.PeekInstruction()));

        ViewOpcode(addr, opcode); // View the opcode we just executed
        opcode = _emulator.PeekInstruction();
        ViewOpcode(_emulator.CPU.Registers.PC, opcode); // View the next opcode
        AnsiConsole.WriteLine();
        ViewRegisters();
        return IsTopLevelReturn(opcode);
    }

    static void ViewHelp()
    {
        AnsiConsole.MarkupLine("[blue]h[/][silver]elp[/]");
        AnsiConsole.MarkupLine("[blue]s[/][silver]tep[/]");
        AnsiConsole.MarkupLine("[blue]r[/][silver]un[/]");
        AnsiConsole.MarkupLine("[blue]reg[/][silver]isters[/]");
        AnsiConsole.MarkupLine("[blue]m[/][silver]emory[/] [yellow][[address]][/]");
        AnsiConsole.MarkupLine("[blue]d[/][silver]isassemble[/] [yellow][[address]][/]");
        AnsiConsole.MarkupLine("[blue]b[/][silver]reakpoints[/]");
        AnsiConsole.MarkupLine("[blue]reset[/]");
        AnsiConsole.MarkupLine("[blue]q[/][silver]uit[/]");
        AnsiConsole.WriteLine();
    }

    void ViewRegisters()
    {
        _lastMemAddr = null;
        _lastDisAddr = null;
        var r = _emulator.CPU.Registers;
        AnsiConsole.MarkupLine("[blue] PC   AC XR YR SR    SP | NV-BDIZC[/]");
        AnsiConsole.MarkupLine($"[aqua]{r.PC:X4}  {r.A:X2} {r.X:X2} {r.Y:X2} {r.S:X2}[/] [blue]=>[/] [green]{_emulator.Memory[0x100 + r.S]:X2}[/] [blue]|[/] [aqua]{r.P:B8}[/]");
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// View memory starting at startAddr and return the next address to view
    /// </summary>
    /// <param name="startAddr">Address to start viewing</param>
    /// <param name="len">The length in bytes to view</param>
    /// <returns></returns>
    void ViewMemory(word startAddr = 0x0200, word len = 0x60)
    {
        startAddr = (word)(startAddr / 0x10 * 0x10);
        for (word addr = startAddr; addr < startAddr + len; addr += 0x10)
        {
            AnsiConsole.Markup($"[blue]{addr:X4}[/] ");
            for (word i = 0; i <= 0xF; i++)
            {
                string color = _breakpoints.Contains((word)(addr + i)) ? "red" : "aqua";
                AnsiConsole.Markup($"[{color}]{_emulator.Memory[addr + i]:X2}[/] ");
            }
            for (word i = 0; i <= 0xF; i++)
            {
                char c = (char)_emulator.Memory[addr + i];
                c = char.IsControl(c) ? '.' : c;
                string s = c.ToString();
                if (s == "[") s = "[[";
                else if (s == "]") s = "]]";
                AnsiConsole.Markup($"[green]{s}[/]");
            }
            AnsiConsole.WriteLine();
        }
        _lastMemAddr = (ushort)(startAddr + len);
        _lastDisAddr = null;
    }

    /// <summary>
    /// View the disassembly of the program from the current PC
    /// </summary>
    /// <param name="startAddr">Address to start disassembly</param>
    /// <param name="len">Number of instructions to disassemble</param>
    void ViewDisassembly(word startAddr, word len = 12)
    {
        word addr = startAddr;
        for (int count = 0; count < len; count++)
        {
            try
            {
                Opcode opcode = _emulator.Disassemble(addr);
                ViewOpcode(addr, opcode);
                addr += opcode.Length;

            }
            catch (Exception)
            {
                ViewOpcode(addr, null);
                addr++;
            }
        }
        AnsiConsole.WriteLine();

        _lastMemAddr = null;
        _lastDisAddr = addr;
    }

    bool IsBreakpointSet(ushort addr, Opcode opcode)
    {
        bool breakpoint = false;
        for (int i = 0; i < opcode.Length; i++)
        {
            if (_breakpoints.Contains((word)(addr + i)))
            {
                breakpoint = true;
                break;
            }
        }

        return breakpoint;
    }

    void ManageBreakpoints()
    {
        _lastMemAddr = null;
        _lastDisAddr = null;
        while (true)
        {
            string command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Manage breakpoints[/]")
                    .AddChoices(new[] { "add", "delete", "clear", "list", "quit" }));

            switch (command)
            {
                case "add":
                    AddBreakpoint();
                    break;
                case "delete":
                    DeleteBreakpoint();
                    break;
                case "clear":
                    ClearBreakpoints();
                    break;
                case "list":
                    ListBreakpoints();
                    break;
                case "quit":
                    return;
            }
        }
    }

    private void AddBreakpoint()
    {
        string addr = AnsiConsole.Ask<string>("Address to break on (in HEX): ");
        if (addr.TryParseHexWord(out ushort breakpoint))
        {
            _breakpoints.Add(breakpoint);
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Invalid address[/]");
        }
    }

    private void DeleteBreakpoint()
    {
        var delete = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(10)
                .Title("[blue]Select breakpoint to delete[/]")
                .AddChoices(_breakpoints.Select(b => $"0x{b:X4}")));

        if (delete.TryParseHexWord(out ushort breakpoint))
        {
            _breakpoints.Remove(breakpoint);
        }
    }

    private void ClearBreakpoints()
    {
        _breakpoints.Clear();
    }

    private void ListBreakpoints()
    {
        if (_breakpoints.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No breakpoints set[/]");
            return;
        }
        string breakpointList = string.Join(", ", _breakpoints.Select(b => $"0x{b:X4}"));
        AnsiConsole.MarkupLine($"[aqua]{breakpointList}[/]");
    }
}
