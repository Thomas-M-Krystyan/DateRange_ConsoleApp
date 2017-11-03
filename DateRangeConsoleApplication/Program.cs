using DateRangeConsoleApplication.Controllers;

namespace DateRangeConsoleApplication
{
    internal static class Program
    {
        // Valid number of arguments. Change if you want
        private const int NumberOfArguments = 2;

        private static void Main(string[] arguments)
        {
            var application = new ApplicationController<string, int>();
            application.Start(arguments, NumberOfArguments);
        }
    }
}
