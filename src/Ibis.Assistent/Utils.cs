namespace Ibis.Assistent;

public static class Utils
{
    public static void PrintWithColor(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}