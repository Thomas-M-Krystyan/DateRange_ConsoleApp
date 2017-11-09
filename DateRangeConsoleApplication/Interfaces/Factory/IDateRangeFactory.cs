using System.Globalization;
using DateRangeConsoleApplication.Interfaces.Factory.DateRange;

namespace DateRangeConsoleApplication.Interfaces.Factory
{
    public interface IDateRangeFactory
    {
        IDateRange From(string[] stringArray, CultureInfo currentCulture);
    }
}
