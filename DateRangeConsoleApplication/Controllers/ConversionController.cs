using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateRangeConsoleApplication.Controllers
{
    internal sealed class ConversionController<T, TN> : ValidationController<T, TN>
                                                        where T : IComparable
    {
        // Constants
        private const string DateTimeIsShort = "short";
        private const string DateTimeIsLong = "long";

        // Controllers
        internal IList<T> ProcessInputData(IList<T> collection)
        {
            IList<T> parsedCollection = ParseToSpecificCollection(collection);
            ConvertInputsToDateTime(collection, ref parsedCollection);

            return null;
        }

        // Methods
        private static void ConvertInputsToDateTime(IList<T> collection, ref IList<T> parsedCollection)
        {
            CultureInfo currentCulture = CultureInfo.CurrentUICulture;

            DateTime date = new DateTime();
            foreach (var element in collection)
            {
                if (RecogniseDateFormat(element, currentCulture, out date) == DateTimeIsShort)
                {
                    date.ToShortDateString();
                }
                else if (RecogniseDateFormat(element, currentCulture, out date) == DateTimeIsLong)
                {
                    date.ToLongDateString();
                }
            }
        }

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

        private static void GenericAdd(T element, ref IList<T> parsedCollection)
        {
            if (parsedCollection is T[])
            {
            }
            else if (parsedCollection is List<T>)
            {
                parsedCollection.Add(element);
            }
        }

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
    }
}
