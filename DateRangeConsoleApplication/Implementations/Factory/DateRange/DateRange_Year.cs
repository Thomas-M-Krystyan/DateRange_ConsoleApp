using System;
using System.Globalization;
using DateRangeConsoleApplication.Interfaces.Factory.DateRange;

namespace DateRangeConsoleApplication.Implementations.Factory.DateRange
{
    internal class DateRangeSameYear : IDateRange
    {
        private readonly string _hyphen;
        private readonly string _formatStyle;
        private readonly DateTime _firstDate;
        private readonly DateTime _lastDate;
        private readonly CultureInfo _currentCulture;
        private readonly string _dateSeparator;

        internal DateRangeSameYear(string hyphen, string formatStyle, DateTime firstDate, DateTime lastDate,
                                   CultureInfo currentCulture, string dateSeparator)
        {
            this._hyphen = hyphen;
            this._formatStyle = formatStyle;
            this._firstDate = firstDate;
            this._lastDate = lastDate;
            this._currentCulture = currentCulture;
            this._dateSeparator = dateSeparator;
        }

        public override string ToString()
        {
            string firstDateWithoutYear = DateRangeFactory.GetDateWithoutYearFrom(this._formatStyle, this._firstDate,
                                                                                  this._dateSeparator, this._currentCulture);
            string lastDateWithoutYear = DateRangeFactory.GetDateWithoutYearFrom(this._formatStyle, this._lastDate,
                                                                                 this._dateSeparator, this._currentCulture);

            // Date formats: YY(YYYY)-D(DD)-M(MM) or YY(YYYY)-M(MM)-D(DD)
            bool dateStartsFromYear = DateRangeFactory.IsDateFormatBeginsFromYear(this._currentCulture);
            if (dateStartsFromYear)
            {
                return $"{this._lastDate.Year}{this._dateSeparator}{firstDateWithoutYear} {this._hyphen} {lastDateWithoutYear}";
            }

            // Date formats: D(DD)-M(MM)-YY(YYYY) or M(MM)-D(DD)-YY(YYYY)
            return $"{firstDateWithoutYear} {this._hyphen} {this._lastDate.ToString(this._formatStyle, this._currentCulture)}";
        }
    }
}
