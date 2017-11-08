using System;
using System.Globalization;
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
        private readonly string _dateSeparator;

        internal DateRangeSameMonth(string hyphen, string formatStyle, DateTime firstDate, DateTime lastDate,
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
            string cultureFirstDay = DateRangeFactory.GetCultureDayFrom(this._firstDate, this._currentCulture);
            string cultureLastDay = DateRangeFactory.GetCultureDayFrom(this._lastDate, this._currentCulture);
            string firstDateWithoutYear = DateRangeFactory.GetDateWithoutYearFrom(this._formatStyle, this._firstDate,
                                                                                  this._dateSeparator, this._currentCulture);
            
            // Date formats: YY(YYYY)-M(MM)-D(DD)
            bool dateStartsFromYear = DateRangeFactory.IsDateFormatBeginsFromYear(this._currentCulture);
            if (dateStartsFromYear)
            {
                return $"{this._firstDate.Year}{this._dateSeparator}{firstDateWithoutYear}{this._hyphen}{cultureLastDay}";
            }

            // Date formats: M(MM)-D(DD)-YY(YYYY)
            bool dateStartsFromMonth = DateRangeFactory.IsDateFormatBeginsFromMonth(this._currentCulture);
            if (dateStartsFromMonth)
            {
                return $"{this._firstDate.Month}{this._dateSeparator}{cultureFirstDay}{this._hyphen}" +
                       $"{cultureLastDay}{this._dateSeparator}{this._firstDate.Year}";
            }

            // Date formats: D(DD)-M(MM)-YY(YYYY)
            return $"{cultureFirstDay}{this._hyphen}{this._lastDate.ToString(this._formatStyle, this._currentCulture)}";
        }
    }
}
