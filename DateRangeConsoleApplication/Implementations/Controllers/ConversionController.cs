using System;
using System.Collections.Generic;
using System.Globalization;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Implementations.Controllers
{
    public class ConversionController
    {
        private readonly CultureInfo _currentCulture;

        public ConversionController(CultureInfo currentCulture)
        {
            this._currentCulture = currentCulture;
        }

        public DateTime[] ProcessInputArray(string[] inputArray)
        {
            DateTime[] convertedDateArray = ConvertStringsToDateTime(inputArray);
            
            return convertedDateArray;
        }

        private DateTime[] ConvertStringsToDateTime(IReadOnlyList<string> inputArray)
        {
            bool collectionNotExist = Equals(inputArray, null);
            if (collectionNotExist)
            {
                throw new ArgumentNullException(nameof(inputArray), DisplayController.SetMessageColor(ErrorNullCollection,
                                                DisplayController.Color.DarkRed));
            }

            int inputArrayLength = inputArray.Count;
            DateTime[] convertedDateArray = new DateTime[inputArrayLength];

            DateTime date;
            for (int i = 0; i < inputArrayLength; i++)
            {
                ValidationController.TryParseExactToDate(inputArray[i], this._currentCulture, out date);
                convertedDateArray[i] = date;
            }

            return convertedDateArray;
        }
    }
}
