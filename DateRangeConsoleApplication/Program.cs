using DateRangeConsoleApplication.Implementations.Controllers;

namespace DateRangeConsoleApplication
{
    internal static class Program
    {
        private static void Main(string[] arguments)
        {
            var application = new ApplicationController();
            application.Start(new string[] {});
        }
    }
}
