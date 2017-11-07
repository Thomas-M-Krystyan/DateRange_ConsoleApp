using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using DateRangeConsoleApplication.UI;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ValidationController
    {
        private delegate void ParamsAction(params object[] parameters);

        internal DateTime[] CheckInputData(string[] inputArray, CultureInfo currentCulture)
        {
            ParamsAction validationCriteria = delegate { ValidNumberOfArguments(inputArray); };

            validationCriteria += delegate { ValidDateTimeFormat(inputArray, currentCulture); };

            ConversionController converter = new ConversionController();
            DateTime[] dateArray = converter.ProcessInputData(inputArray, currentCulture);
            validationCriteria += delegate { CompareDateTimeValues(dateArray); };

            if (ValidationResult(validationCriteria, new object[] { inputArray }))
            {
                return dateArray;
            }
            throw new ValidationException(Utilities.DisplayInColor(message: ErrorValidationFailed, color: "red"));
        }

        #region Handling validation exceptions
        private static bool ValidationResult(ParamsAction validationCriteria, object[] parameters)
        {
            try
            {
                validationCriteria(parameters);
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
                return false;
            }
        }
        #endregion

        #region Validation: Number of arguments
        private static void ValidNumberOfArguments(string[] inputArray)
        {
            if (Equals(inputArray, null))
            {
                throw new ArgumentNullException(nameof(inputArray),
                                                Utilities.DisplayInColor(message: ErrorNullCollection));
            }
            if (!inputArray.Any())
            {
                throw new ArgumentException(Utilities.DisplayInColor(message: ErrorEmptyCollection),
                                            nameof(inputArray));
            }
        }
        #endregion

        #region Validation: Proper date format
        private static void ValidDateTimeFormat(IEnumerable<string> inputArray, CultureInfo currentCulture)
        {
            foreach (var element in inputArray)
            {
                if (!TryParseDateTime(element, currentCulture, out DateTime date))
                {
                    throw new FormatException(Utilities.DisplayInColor(message: ErrorWrongInputFormat(element, currentCulture)));
                }
            }
        }

        /// <summary>
        /// Checks if given input is DateTime type in one of two formats (short or long) and returns parsed input
        /// </summary>
        public static bool TryParseDateTime(string element, IFormatProvider currentCulture, out DateTime date)
        {
            return DateTime.TryParse(element, currentCulture, DateTimeStyles.None, out date);
        }
        #endregion

        #region Validation: Compare date objects
        //
        private static void CompareDateTimeValues(IEnumerable<DateTime> dateArray)
        {
            DateTime? previousDate = null;
            foreach (var date in dateArray)
            {
                if (previousDate > date)
                {
                    throw new ArgumentException(Utilities.DisplayInColor(
                        message: ErrorUnexpectedDateOrder(previousDate?.ToShortDateString(),date.ToShortDateString())));
                }
                previousDate = date;
            }
        }
        #endregion
    }
}
