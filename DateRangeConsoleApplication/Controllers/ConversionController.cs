using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ConversionController
    {
        internal DateTime[] ProcessInputData(string[] inputArray, CultureInfo currentCulture)
        {
            DateTime[] convertedDateArray = ConvertInputsToDateTime(inputArray, currentCulture);
            
            return convertedDateArray;
        }

        private static DateTime[] ConvertInputsToDateTime(IReadOnlyList<string> inputArray, IFormatProvider currentCulture)
        {
            DateTime[] convertedDateArray = new DateTime[inputArray.Count];

            DateTime date;
            for (int i = 0; i < inputArray.Count; i++)
            {
                ValidationController.TryParseDateTime(inputArray[i], currentCulture, out date);
                convertedDateArray[i] = date;
            }

            return convertedDateArray;
        }
    }
}
