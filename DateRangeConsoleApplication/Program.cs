using System;
using System.Collections.Generic;
using DateRangeConsoleApplication.Controllers;

namespace DateRangeConsoleApplication
{
    internal static class Program
    {
        // Constants
        private const int NumberOfArguments = 2;  // Valid number of arguments

        // Methods
        private static void Main(string[] arguments)
        {
            var application = new ApplicationController<string, int>();
            application.Start(new List<string>() {"2017-3/03", "3 listopadda 2017"}, NumberOfArguments);

            Console.ReadKey();
        }
    }
}
