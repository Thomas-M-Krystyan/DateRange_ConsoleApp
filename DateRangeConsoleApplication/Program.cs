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
            application.Start(new string[] {"2017-11-03", "3 listopada 2017"}, NumberOfArguments);
        }
    }
}
