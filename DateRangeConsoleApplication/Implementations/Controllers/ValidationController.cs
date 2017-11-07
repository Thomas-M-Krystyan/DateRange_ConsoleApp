using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using DateRangeConsoleApplication.UI.Messages;

namespace DateRangeConsoleApplication.Implementations.Controllers
{
    internal class ValidationController
    {
        private delegate void ParamsAction(params object[] parameters);

        internal DateTime[] CheckInputArray(string[] inputArray, CultureInfo currentCulture)
        {
            ParamsAction validationCriteria = delegate { IsCollectionValid(inputArray); };

            validationCriteria += delegate { IsInputValid(inputArray, currentCulture); };

            ConversionController converter = new ConversionController();
            DateTime[] dateArray = converter.ProcessInputArray(inputArray, currentCulture);
            validationCriteria += delegate { IsDatesOrderAscending(dateArray); };

            if (IsValidationSucceed(validationCriteria, new object[] { inputArray }))
            {
                return dateArray;
            }
            throw new ValidationException(DisplayController.ApplyColorToMessage(EnglishMessages.ErrorValidationFailed, DisplayController.Color.DarkRed));
        }

        #region Handling validation exceptions
        private static bool IsValidationSucceed(ParamsAction validationCriteria, object[] parameters)
        {
            try
            {
                validationCriteria(parameters);
                return true;
            }
            catch (Exception exception)
            {
                DisplayController.Display(exception.Message);
                Console.ReadKey();
                return false;
            }
        }
        #endregion

        #region Validation: Proper collection
        private static bool IsCollectionValid(string[] inputArray)
        {
            bool collectionNotExist = Equals(inputArray, null);
            if (collectionNotExist)
            {
                throw new ArgumentNullException(nameof(inputArray), DisplayController.ApplyColorToMessage(EnglishMessages.ErrorNullCollection, DisplayController.Color.DarkRed));
            }
            bool collectionIsEmpty = !inputArray.Any();
            if (collectionIsEmpty)
            {
                throw new ArgumentException(DisplayController.ApplyColorToMessage(EnglishMessages.ErrorEmptyCollection, DisplayController.Color.DarkRed), nameof(inputArray));
            }

            return true;
        }
        #endregion

        #region Validation: Proper date format
        private static bool IsInputValid(IEnumerable<string> inputArray, CultureInfo currentCulture)
        {
            foreach (var element in inputArray)
            {
                bool inputCannotBeParsedToDate = !TryParseToDate(element, currentCulture, out DateTime date);
                if (inputCannotBeParsedToDate)
                {
                    throw new FormatException(DisplayController.ApplyColorToMessage(EnglishMessages.ErrorWrongInputFormat(element, currentCulture), DisplayController.Color.DarkRed));
                }
            }

            return true;
        }

        internal static bool TryParseToDate(string element, IFormatProvider currentCulture, out DateTime date)
        {
            return DateTime.TryParse(element, currentCulture, DateTimeStyles.AssumeLocal, out date);
        }
        #endregion

        #region Validation: Compare date objects
        //
        private static bool IsDatesOrderAscending(IEnumerable<DateTime> dateArray)
        {
            DateTime? previousDate = null;
            foreach (var date in dateArray)
            {
                bool previousDateIsLaterThanNextDate = previousDate > date;
                if (previousDateIsLaterThanNextDate)
                {
                    throw new ArgumentException(DisplayController.ApplyColorToMessage(EnglishMessages.ErrorUnexpectedDateOrder(previousDate?.ToShortDateString(),
                                                                                    date.ToShortDateString()), DisplayController.Color.DarkRed));
                }
                previousDate = date;
            }

            return true;
        }
        #endregion
    }
}
