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
        // Delegates
        private delegate void ParamsAction(params object[] parameters);

        // Controllers
        internal IList<DateTime> CheckInputData(IList<string> collection, CultureInfo currentCulture)
        {
            // Add new validation method
            ParamsAction validationCriteria = delegate { ValidNumberOfArguments(collection); };

            // Add new validation method
            validationCriteria += delegate { ValidDateTimeFormat(collection, currentCulture); };

            // Add new validation method
            ConversionController converter = new ConversionController();
            IList<DateTime> dateCollection = converter.ProcessInputData(collection, currentCulture);
            validationCriteria += delegate { CompareDateTimeValues(dateCollection); };

            if (ValidationResult(validationCriteria, new object[] { collection }))
            {
                return dateCollection;
            }
            throw new ValidationException(Utilities.DisplayInColor(message: ErrorValidationFailed, color: "red"));
        }

        // Methods
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
        private static void ValidNumberOfArguments(IList<string> collection)
        {
            if (Equals(collection, null))
            {
                throw new ArgumentNullException(nameof(collection),
                                                Utilities.DisplayInColor(message: ErrorNullCollection));
            }
            if (!collection.Any())
            {
                throw new ArgumentException(Utilities.DisplayInColor(message: ErrorEmptyCollection),
                                            nameof(collection));
            }
        }
        #endregion

        #region Validation: Proper date format
        private static void ValidDateTimeFormat(IEnumerable<string> collection, CultureInfo currentCulture)
        {
            foreach (var element in collection)
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
        private static void CompareDateTimeValues(IEnumerable<DateTime> dateCollection)
        {
            DateTime? previousDate = null;
            foreach (var date in dateCollection)
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
