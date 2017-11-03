using DateRangeConsoleApplication.Controllers;

namespace DateRangeConsoleApplication
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            ApplicationController application = new ApplicationController();
            application.Start(args);
        }
    }
}
