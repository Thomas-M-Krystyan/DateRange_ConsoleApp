using DateRangeConsoleApplication.Implementations.Controllers;

namespace DateRangeConsoleApplication
{
    internal static class Program
    {
        private static void Main(string[] arguments)
        {
            string[] _args = { "2 2 2017", "3 5 2017" };

            var application = new ApplicationController();
            application.Start(_args);
        }
    }
}
