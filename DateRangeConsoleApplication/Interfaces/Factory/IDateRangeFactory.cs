using System.Globalization;
using DateRangeConsoleApplication.Interfaces.Factory.DateRange;

namespace DateRangeConsoleApplication.Interfaces.Factory
{
    internal interface IDateRangeFactory
    {
        IDateRange From(string[] stringArray, CultureInfo currentCulture);
    }
}
