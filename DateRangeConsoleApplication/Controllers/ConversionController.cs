using System;
using System.Collections.Generic;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ConversionController<T, TN> where T : IComparable
    {
        // Controllers
        internal IList<T> ProcessInputData(IList<T> collection)
        {
            IList<T> convertedDateCollection = ParseToSpecificCollection(collection);

            return null;
        }

        // Methods

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
