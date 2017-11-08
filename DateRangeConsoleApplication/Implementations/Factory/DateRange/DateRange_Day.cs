using System;
using System.Globalization;
using DateRangeConsoleApplication.Interfaces.Factory.DateRange;

namespace DateRangeConsoleApplication.Implementations.Factory.DateRange
{
    internal class DateRangeSameDay : IDateRange
    {
        private readonly string _formatStyle;
        private readonly DateTime _firstDate;
        private readonly CultureInfo _currentCulture;

        internal DateRangeSameDay(string formatStyle, DateTime firstDate, CultureInfo currentCulture)
        {
            this._formatStyle = formatStyle;
            this._firstDate = firstDate;
            this._currentCulture = currentCulture;
        }

        public override string ToString()
        {
            return $"{this._firstDate.ToString(this._formatStyle, this._currentCulture)}";
        }
    }
}
