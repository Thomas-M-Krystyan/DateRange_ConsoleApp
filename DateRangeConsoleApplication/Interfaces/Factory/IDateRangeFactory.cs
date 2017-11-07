using DateRangeConsoleApplication.Interfaces.Factory.Range;

namespace DateRangeConsoleApplication.Interfaces.Factory
{
    internal interface IDateRangeFactory
    {
        IDateRange From(string[] arguments);
    }
}
