using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ConversionController<T, TN> : ValidationController<T, TN> where T : IComparable
    {
        // Controllers
        internal IList<T> ProcessInputData(IList<T> collection, CultureInfo currentCulture)
        {
            IList<DateTime> convertedDateCollection = ConvertInputsToDateTime(collection, currentCulture);
            
            return null;
        }

        // Methods
        private static IList<DateTime> ConvertInputsToDateTime(IList<T> collection, CultureInfo currentCulture)
        {
            bool collectionIsReadOnly;
            IList<DateTime> convertedDateCollection = ParseToSpecificCollection(collection, out collectionIsReadOnly);

            DateTime date;
            for (int i = 0; i < collection.Count; i++)
            {
                if (collectionIsReadOnly)
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
        
        /// <summary>
        /// Returns specific instance of generic collection and information
        /// if IsReadOnly, based on input collection type (e.g. array / list)
        /// </summary>
        private static IList<DateTime> ParseToSpecificCollection(IList<T> collection, out bool collectionIsReadOnly)
        {
            IList<DateTime> parsedCollection;
            
            if (collection.IsReadOnly)
            {
                collectionIsReadOnly = true;
                parsedCollection = new DateTime[collection.Count];
            }
            else
            {
                collectionIsReadOnly = false;
                parsedCollection = new List<DateTime>();
            }

            return parsedCollection;
        }
    }
}
