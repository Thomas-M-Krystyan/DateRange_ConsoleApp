using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using DateRangeConsoleApplication.Controllers;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ConversionController
    {
        // Controllers
        internal IList<DateTime> ProcessInputData(IList<string> collection, CultureInfo currentCulture)
        {
            IList<DateTime> convertedDateCollection = ConvertInputsToDateTime(collection, currentCulture);
            
            return convertedDateCollection;
        }

        // Methods
        private static IList<DateTime> ConvertInputsToDateTime(IList<string> collection, IFormatProvider currentCulture)
        {
            IList<DateTime> convertedDateCollection = new Collection<DateTime>();

            DateTime date;
            for (int i = 0; i < collection.Count; i++)
            {
                ValidationController.TryParseDateTime(collection[i], currentCulture, out date);

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
