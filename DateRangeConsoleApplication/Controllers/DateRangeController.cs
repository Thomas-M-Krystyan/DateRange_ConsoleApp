using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace DateRangeConsoleApplication.Controllers
{
    internal class DateRangeController
    {
        // Enums
        private enum Similarity { NoSimilarity, SameYear, SameMonth, SameDay }

        // Controllers
        internal string GenerateRange(IList<DateTime> dateCollection, CultureInfo currentCulture)
        {
            var checkingFuncCriteriaList = PrepareCheckingFuncCriteriaList();
            Console.WriteLine(CheckDateSimilarity(dateCollection, checkingFuncCriteriaList));

            Console.ReadKey();

            return null;
        }

        // Methods
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
        private static Similarity CheckDateSimilarity(ICollection<DateTime> dateCollection,
                                                      IEnumerable<Func<DateTime, DateTime, Similarity>> checkingFuncCriteriaList)
        {
            if (dateCollection.Count == 1)
            {
                return Similarity.SameDay;
            }
            HashSet<Similarity> checkingResultsSet = new HashSet<Similarity>();
            Similarity finalComparisonResult = Similarity.NoSimilarity;

            foreach (var checkingCriterium in checkingFuncCriteriaList)
            {
                DateTime? previousDate = null;
                Similarity currentComparisonResult;
                foreach (var date in dateCollection)
                {
                    if (previousDate != null)
                    {
                        currentComparisonResult = checkingCriterium((DateTime)previousDate, date);
                        checkingResultsSet.Add(currentComparisonResult);
                    }
                    previousDate = date;
                }
                currentComparisonResult = checkingResultsSet.Count == 1 ? checkingResultsSet.First()
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
    }
}
