using System;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.UI
{
    internal static class Utilities
    {
        // Constants
        private const string ErrorMessageColor = "darkred";

        // Methods
        internal static string DisplayInColor(string message, string color = ErrorMessageColor)
        {
            switch (color.ToLower())
            {
                case "darkred":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "darkyellow":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "darkgreen":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                default:
                    throw new ArgumentException(DisplayInColor(message: ErrorWrongMessageColor));
            }

            return message;
        }
    }
}
