using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ConversionController<T, TN> : ValidationController<T, TN> where T : IComparable
    {
        // Controllers
        internal IList<DateTime> ProcessInputData(IList<T> collection, CultureInfo currentCulture)
        {
            IList<DateTime> convertedDateCollection = ConvertInputsToDateTime(collection, currentCulture);
            
            foreach (var element in convertedDateCollection) {
                Console.WriteLine(element);
            }
            Console.ReadLine();
            return convertedDateCollection;
        }

        // Methods
        private static IList<DateTime> ConvertInputsToDateTime(IList<T> collection, CultureInfo currentCulture)
        {
            IList<DateTime> convertedDateCollection = new Collection<DateTime>();

            DateTime date;
            for (int i = 0; i < collection.Count; i++)
            {
                if (convertedDateCollection.IsReadOnly)
                {
                    TryParseExactDateTime(collection[i], currentCulture, out date);
                    convertedDateCollection[i] = date;
                }
                else
                {
                    TryParseExactDateTime(collection[i], currentCulture, out date);
                    convertedDateCollection.Add(date);
                }
            }

            return convertedDateCollection;
        }
    }
}
