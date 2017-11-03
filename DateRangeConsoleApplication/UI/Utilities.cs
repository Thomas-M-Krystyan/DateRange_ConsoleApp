using System;

namespace DateRangeConsoleApplication.UI
{
    internal static class Utilities
    {
        // Methods
        internal static string DisplayColor(string message, string color)
        {
            switch (color.ToLower())
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            return message;
        }
    }
}
