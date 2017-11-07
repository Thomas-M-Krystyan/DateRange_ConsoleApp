using DateRangeConsoleApplication.Controllers;

namespace DateRangeConsoleApplication
{
    internal static class Program
    {
        static string[] _args = {"2017-11-04", "2017-12-05"};

        // Methods
        private static void Main(string[] arguments)
        {
            var application = new ApplicationController();
            application.Start(_args);
        }
    }
}
