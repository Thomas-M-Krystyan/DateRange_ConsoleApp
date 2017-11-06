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
            application.Start(new string[] {"2017-10-05", "2017-10-04" }, NumberOfArguments);
        }
    }
}
