using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;
using static DateRangeConsoleApplication.Controllers.DisplayController;

namespace DateRangeConsoleApplication.Controllers
{
    internal class DateRangeController
    {
        private enum Similarity { NoSimilarity, SameYear, SameMonth, SameDay }

        internal string AnalyzeData(DateTime[] dateArray, CultureInfo currentCulture)
        {
            IEnumerable<Func<DateTime, DateTime, Similarity>> checkingFuncCriteriaList = PrepareCheckingFuncCriteriaList();
            Similarity checkingResult = CheckDateSimilarity(dateArray, checkingFuncCriteriaList);

            return GenerateRange(dateArray, checkingResult, currentCulture);
        }

        /// <summary> 
        /// Create generic collection of Func delegates used as criteria of checking DateTime type similarity
        /// </summary>
        private static IEnumerable<Func<DateTime, DateTime, Similarity>> PrepareCheckingFuncCriteriaList()
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
        private static Similarity CheckDateSimilarity(DateTime[] dateArray,
                                                      IEnumerable<Func<DateTime, DateTime, Similarity>> checkingFuncCriteriaList)
        {
            if (!dateArray.Any())
            {
                return Similarity.SameDay;
            }
            HashSet<Similarity> checkingResultsSet = new HashSet<Similarity>();
            Similarity finalComparisonResult = Similarity.NoSimilarity;

            foreach (var checkingCriterium in checkingFuncCriteriaList)
            {
                DateTime? previousDate = null;
                Similarity currentComparisonResult;
                //
                foreach (var date in dateArray)
                {
                    if (previousDate != null)
                    {
                        currentComparisonResult = checkingCriterium((DateTime)previousDate, date);
                        checkingResultsSet.Add(currentComparisonResult);
                    }
                    previousDate = date;
                }
                //
                currentComparisonResult = checkingResultsSet.Count == 1 ? checkingResultsSet.First()
                                                                        : Similarity.NoSimilarity;

                //
                if (currentComparisonResult != Similarity.NoSimilarity)
                {
                    finalComparisonResult = currentComparisonResult;
                }
                else
                {
                    return finalComparisonResult;
                }
                //
                checkingResultsSet.Clear();
            }

            return finalComparisonResult;
        }

        private static string GenerateRange(DateTime[] dateArray, Similarity checkingResult, CultureInfo currentCulture)
        {
            const string hyphen = "\u2014";
            const string formatStyle = "d";

            DateTime firstDate = dateArray.First();
            DateTime lastDate = dateArray.Last();
            string dateSeparator = currentCulture.DateTimeFormat.DateSeparator;

            switch (checkingResult)
            {
                // 01.05.2017
                case Similarity.SameDay:
                    return $"{firstDate.ToString(formatStyle, currentCulture)}";
                // 01-05.01.2017
                case Similarity.SameMonth:
                    return $"{GetCultureDay(firstDate, currentCulture)}{hyphen}{lastDate.ToString(formatStyle, currentCulture)}";
                // 01.01 – 05.02.2017
                case Similarity.SameYear:
                    string dateStr = firstDate.ToString(formatStyle, currentCulture).Replace(firstDate.ToString("yyyy",
                                     currentCulture), string.Empty).Trim(Convert.ToChar(dateSeparator));
                    return $"{dateStr} {hyphen} " + $"{lastDate.ToString(formatStyle, currentCulture)}";
                // 01.01.2016 – 05.01.2017
                case Similarity.NoSimilarity:
                    return $"{firstDate.ToString(formatStyle, currentCulture)} {hyphen} {lastDate.ToString(formatStyle, currentCulture)}";
                default:
                    throw new ArgumentOutOfRangeException(ApplyColorToMessage(ErrorInvalidFormatStrategy, Color.DarkRed));
            }
        }

        private static string GetCultureDay(DateTime date, CultureInfo currentCulture)
        {
            string shortDateFormat = currentCulture.DateTimeFormat.ShortDatePattern;
            int letterCount = shortDateFormat.Count(letter => letter == 'd');

            return letterCount == 1 ? date.Day.ToString() : date.ToString("dd", currentCulture);
        }
    }
}
