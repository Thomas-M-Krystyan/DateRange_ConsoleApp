using System;
using System.Globalization;

namespace DateRangeConsoleApplication
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("The current culture is {0}", CultureInfo.CurrentCulture.DisplayName);
            CultureInfo.CurrentCulture = new CultureInfo("en-GB");
            Console.WriteLine("The current culture is {0}", CultureInfo.CurrentCulture.DisplayName);
            Console.ReadLine();

            DateTime time = DateTime.Now;
            Console.WriteLine(time.ToString("dd.MM.yyyy"));

            if (args == null)
            {
                Console.WriteLine("ERROR: Null!");
            }
            else if (args.Length < 2)
            {
                Console.WriteLine("ERROR: Less than two elements!");
            }
            else if (args.Length > 1 && args.Length < 3)
            {
                Console.WriteLine("OK: Two elements");
                foreach (string element in args)
                {
                    Console.WriteLine(element);
                }
            }
            else
            {
                Console.WriteLine("ERROR: More than two elements!");
            }
            Console.ReadLine();
        }
    }
}
