namespace Mos6502Emu.Tests.Extensions;

/// <summary>
/// Extension methods to provide Shouldly compatibility with primitive types
/// </summary>
public static class ShouldlyExtensions
{
    /// <summary>
    /// Assert that a byte equals the expected value
    /// </summary>
    public static void ShouldBe(this byte actual, byte expected, string? customMessage = null)
    {
        ((int)actual).ShouldBe(expected, customMessage);
    }

    /// <summary>
    /// Assert that a word equals the expected value
    /// </summary>
    public static void ShouldBe(this word actual, word expected, string? customMessage = null)
    {
        ((int)actual).ShouldBe(expected, customMessage);
    }
}
