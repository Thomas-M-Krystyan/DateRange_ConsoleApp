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
            
            return convertedDateCollection;
        }

        // Methods
        private static IList<DateTime> ConvertInputsToDateTime(IList<T> collection, IFormatProvider currentCulture)
        {
            IList<DateTime> convertedDateCollection = new Collection<DateTime>();

            DateTime date;
            for (int i = 0; i < collection.Count; i++)
            {
                TryParseDateTime(collection[i], currentCulture, out date);

                if (convertedDateCollection.IsReadOnly)
                {
                    convertedDateCollection[i] = date;
                }
                else
                {
                    convertedDateCollection.Add(date);
                }
            }

            return convertedDateCollection;
        }
    }
}
