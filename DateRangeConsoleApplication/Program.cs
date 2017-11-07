using DateRangeConsoleApplication.Controllers;

namespace DateRangeConsoleApplication
{
    internal static class Program
    {
        private static void Main(string[] arguments)
        {
            string[] _args = { "11 lis 2017", "12 lis 2018" };

            var application = new ApplicationController();
            application.Start(_args);
        }
    }
}
