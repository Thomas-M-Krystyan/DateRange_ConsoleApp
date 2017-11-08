using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using DateRangeConsoleApplication.Implementations.Controllers;
using DateRangeConsoleApplication.Implementations.Factory.DateRange;
using DateRangeConsoleApplication.Interfaces.Factory;
using DateRangeConsoleApplication.Interfaces.Factory.DateRange;
using DateRangeConsoleApplication.Interfaces.Validation;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Implementations.Factory
{
    internal class DateRangeFactory : IDateRangeFactory
    {
        private enum Similarity { NoSimilarity, SameYear, SameMonth, SameDay }

        private readonly IValidationController _validationController;

        internal DateRangeFactory(IValidationController validationController)
        {
            this._validationController = validationController;
        }

        public IDateRange From(string[] inputArray, CultureInfo currentCulture)
        {
            DateTime[] dateArray = this._validationController.CheckInputArray(inputArray, currentCulture);

            IEnumerable<Func<DateTime, DateTime, Similarity>> checkingFuncCriteriaList = PrepareComparisonFuncCriteriaList();
            Similarity checkingResult = CheckDateSimilarity(dateArray, checkingFuncCriteriaList);

            return GetRangeFrom(dateArray, checkingResult, currentCulture);
        }

        #region Strategy: Dates ranges checking criteria
        /// <summary> 
        /// Create generic collection of Func delegates used as criteria of checking DateTime type similarity
        /// </summary>
        private IEnumerable<Func<DateTime, DateTime, Similarity>> PrepareComparisonFuncCriteriaList()
        {
            return new Collection<Func<DateTime, DateTime, Similarity>>()
            {
                (previousDate, date) => previousDate.Year == date.Year ? Similarity.SameYear : Similarity.NoSimilarity,
                (previousDate, date) => previousDate.Month == date.Month ? Similarity.SameMonth : Similarity.NoSimilarity,
                (previousDate, date) => previousDate.Day == date.Day ? Similarity.SameDay : Similarity.NoSimilarity
            };
        }

        /// <summary>
        /// Test DateTime elements in collection according to criteria on list of Func delegates
        /// to find the narrowest similarities between them (identical full date, year, or month)
        /// </summary>
        private Similarity CheckDateSimilarity(IReadOnlyCollection<DateTime> dateArray,
                                               IEnumerable<Func<DateTime, DateTime, Similarity>> comparisonFuncCriteriaList)
        {
            if (Equals(dateArray.Count, 1))
            {
                return Similarity.SameDay;
            }
            HashSet<Similarity> checkingResultsSet = new HashSet<Similarity>();
            Similarity finalComparisonResult = Similarity.NoSimilarity;

            foreach (var comparisonCriterium in comparisonFuncCriteriaList)
            {
                Similarity currentComparisonResult = CheckDateBy(comparisonCriterium, dateArray);
                checkingResultsSet.Add(currentComparisonResult);
                currentComparisonResult = Equals(checkingResultsSet.Count, 1) ? checkingResultsSet.First()
                                                                              : Similarity.NoSimilarity;
                if (currentComparisonResult != Similarity.NoSimilarity)
                {
                    finalComparisonResult = currentComparisonResult;
                }
                else
                {
                    return finalComparisonResult;
                }
                checkingResultsSet.Clear();
            }

            return finalComparisonResult;
        }

        private Similarity CheckDateBy(Func<DateTime, DateTime, Similarity> comparisonCriterium, IEnumerable<DateTime> dateArray)
        {
            Similarity currentComparisonResult = Similarity.NoSimilarity;

            DateTime? previousDate = null;
            foreach (var date in dateArray)
            {
                if (previousDate != null)
                {
                    currentComparisonResult = comparisonCriterium((DateTime) previousDate, date);
                }
                previousDate = date;
            }

            return currentComparisonResult;
        }
        #endregion

        #region Factory: Creating date range objects
        private IDateRange GetRangeFrom(DateTime[] dateArray, Similarity similarityResult, CultureInfo currentCulture)
        {
            const string hyphen = "\u2014";
            const string formatStyle = "d";
            DateTime firstDate = dateArray.First();
            DateTime lastDate = dateArray.Last();
            string dateSeparator = currentCulture.DateTimeFormat.DateSeparator;

            switch (similarityResult)
            {
                // 01.05.2017
                case Similarity.SameDay:
                    return new DateRangeSameDay(formatStyle, firstDate, currentCulture);
                // 01-05.01.2017
                case Similarity.SameMonth:
                    return new DateRangeSameMonth(hyphen, formatStyle, firstDate, lastDate, currentCulture, dateSeparator);
                // 01.01 – 05.02.2017
                case Similarity.SameYear:
                    return new DateRangeSameYear(hyphen, formatStyle, firstDate, lastDate, currentCulture, dateSeparator);
                // 01.01.2016 – 05.01.2017
                case Similarity.NoSimilarity:
                    return new DateRangeVarious(hyphen, formatStyle, firstDate, lastDate, currentCulture);
                default:
                    throw new ArgumentOutOfRangeException(DisplayController.SetMessageColor(ErrorInvalidFormatStrategy,
                                                          DisplayController.Color.DarkRed));
            }
        }

        internal static bool IsDateFormatBeginsFromYear(CultureInfo currentCulture)
        {
            const string regexPattern = @"^y{2,4}\W+";

            return IsPatternMatchToCulture(regexPattern, currentCulture);
        }

        internal static bool IsDateFormatBeginsFromMonth(CultureInfo currentCulture)
        {
            const string regexPattern = @"^M{1,3}\W+";

            return IsPatternMatchToCulture(regexPattern, currentCulture);
        }
        
        private static bool IsPatternMatchToCulture(string regexPattern, CultureInfo currentCulture)
        {
            Regex regex = new Regex(regexPattern);
            string shortDate = currentCulture.DateTimeFormat.ShortDatePattern;

            return regex.IsMatch(shortDate);
        }

        internal static string GetCultureDayFrom(DateTime date, CultureInfo currentCulture)
        {
            string shortDateFormat = currentCulture.DateTimeFormat.ShortDatePattern;
            int letterCount = shortDateFormat.Count(letter => letter == 'd');

            return Equals(letterCount, 1) ? date.Day.ToString() : date.ToString("dd", currentCulture);
        }

        internal static string GetDateWithoutYearFrom(string formatStyle, DateTime date,
                                                      string dateSeparator, CultureInfo currentCulture)
        {
            return date.ToString(formatStyle, currentCulture).
                   Replace(date.ToString("yyyy", currentCulture), string.Empty).
                   Trim(Convert.ToChar(dateSeparator));
        }
        #endregion
    }
}
