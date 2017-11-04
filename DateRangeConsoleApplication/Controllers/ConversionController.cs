using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ConversionController<T, TN> where T : IComparable
    {
        // Constants
        private const string DateTimeIsShort = "short";
        private const string DateTimeIsLong = "long";

        // Controllers
        internal IList<T> ProcessInputData(IList<T> collection)
        {
            IList<T> convertedDateCollection = ParseToSpecificCollection(collection);

            return null;
        }

        // Methods
        private static string RecogniseDateFormat(T element, CultureInfo currentCulture, out DateTime date)
        {
            if (DateTime.TryParseExact(element.ToString(), currentCulture.DateTimeFormat.ShortDatePattern,
                currentCulture, DateTimeStyles.AssumeLocal, out date))
            {
                return DateTimeIsShort;
            }
            if (DateTime.TryParseExact(element.ToString(), currentCulture.DateTimeFormat.LongDatePattern,
                currentCulture, DateTimeStyles.AssumeLocal, out date))
            {
                return DateTimeIsLong;
            }

            return string.Empty;
        }
        
        /// <summary>
         /// Returns specific instance of generic collection, based on input collection type (e.g. array / list)
         /// </summary>
        private static IList<T> ParseToSpecificCollection(IList<T> collection)
        {
            IList<T> parsedCollection;

            if (collection.IsReadOnly)
            {
                parsedCollection = new T[collection.Count];
            }
            else
            {
                parsedCollection = new List<T>();
            }

            return parsedCollection;
        }
    }
}
