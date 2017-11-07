using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateRangeConsoleApplication.Implementations.Controllers
{
    internal class ConversionController
    {
        internal DateTime[] ProcessInputArray(string[] inputArray, CultureInfo currentCulture)
        {
            DateTime[] convertedDateArray = ConvertStringsToDateTime(inputArray, currentCulture);
            
            return convertedDateArray;
        }

        private static DateTime[] ConvertStringsToDateTime(IReadOnlyList<string> inputArray, IFormatProvider currentCulture)
        {
            DateTime[] convertedDateArray = new DateTime[inputArray.Count];

            DateTime date;
            for (int i = 0; i < inputArray.Count; i++)
            {
                ValidationController.TryParseToDate(inputArray[i], currentCulture, out date);
                convertedDateArray[i] = date;
            }

            return convertedDateArray;
        }
    }
}
