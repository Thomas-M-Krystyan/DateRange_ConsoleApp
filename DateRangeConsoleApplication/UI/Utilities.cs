using System;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.UI
{
    internal static class Utilities
    {
        internal static string DisplayInColor(string message, string color)
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
                    throw new ArgumentException(DisplayInColor(message: ErrorWrongMessageColor, color: "red"));
            }

            return message;
        }
    }
}
