using System;
using System.Globalization;
using System.Linq;
using DateRangeConsoleApplication.Interfaces.Factory.DateRange;

namespace DateRangeConsoleApplication.Implementations.Factory.DateRange
{
    internal class DateRangeSameMonth : IDateRange
    {
        private readonly string _hyphen;
        private readonly string _formatStyle;
        private readonly DateTime _firstDate;
        private readonly DateTime _lastDate;
        private readonly CultureInfo _currentCulture;

        internal DateRangeSameMonth(string hyphen, string formatStyle, DateTime firstDate,
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
            return $"{GetCultureDay(this._firstDate)}{this._hyphen}" + 
                   $"{this._lastDate.ToString(this._formatStyle, this._currentCulture)}";
        }

        private string GetCultureDay(DateTime date)
        {
            string shortDateFormat = this._currentCulture.DateTimeFormat.ShortDatePattern;
            int letterCount = shortDateFormat.Count(letter => letter == 'd');

            return Equals(letterCount, 1) ? date.Day.ToString() : date.ToString("dd", this._currentCulture);
        }
    }
}
