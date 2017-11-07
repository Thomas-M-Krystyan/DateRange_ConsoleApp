using System;
using DateRangeConsoleApplication.UI.Messages;

namespace DateRangeConsoleApplication.Implementations.Controllers
{
    internal static class DisplayController
    {
        internal enum Color { DarkGreen = 1, DarkYellow = 2, DarkRed = 3 }

        internal static void Display(string message)
        {
            Console.WriteLine(message);
        }

        internal static string ApplyColorToMessage(string message, Color color)
        {
            switch (color)
            {
                case Color.DarkRed:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case Color.DarkYellow:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case Color.DarkGreen:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                default:
                    throw new ArgumentException(ApplyColorToMessage(EnglishMessages.ErrorWrongMessageColor, Color.DarkRed));
            }

            return message;
        }
    }
}
