using System;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Implementations.Controllers
{
    internal static class DisplayController
    {
        internal enum Color { DarkGreen, DarkYellow, DarkRed }

        internal static void Display(string message)
        {
            Console.WriteLine(message);
            Console.ResetColor();
        }

        internal static string SetMessageColor(string message, Color color)
        {
            switch (color)
            {
                case Color.DarkRed:
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case Color.DarkYellow:
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case Color.DarkGreen:
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                default:
                    throw new ArgumentException(SetMessageColor(ErrorWrongMessageColor, Color.DarkRed));
            }

            return message;
        }
    }
}
