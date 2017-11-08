using System;
using System.Globalization;
using DateRangeConsoleApplication.Interfaces.Factory.DateRange;

namespace DateRangeConsoleApplication.Implementations.Factory.DateRange
{
    internal class DateRangeVarious : IDateRange
    {
        private readonly string _hyphen;
        private readonly string _formatStyle;
        private readonly DateTime _firstDate;
        private readonly DateTime _lastDate;
        private readonly CultureInfo _currentCulture;

        internal DateRangeVarious(string hyphen, string formatStyle, DateTime firstDate,
                                  DateTime lastDate, CultureInfo currentCulture)
        {
            this._hyphen = hyphen;
            this._formatStyle = formatStyle;
            this._firstDate = firstDate;
            this._lastDate = lastDate;
            this._currentCulture = currentCulture;
        }

        public override string ToString()
        {
            return $"{this._firstDate.ToString(this._formatStyle, this._currentCulture)} {this._hyphen} " + 
                   $"{this._lastDate.ToString(this._formatStyle, this._currentCulture)}";
        }
    }
}
