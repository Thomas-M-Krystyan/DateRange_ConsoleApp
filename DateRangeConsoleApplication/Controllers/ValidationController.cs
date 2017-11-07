using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using static DateRangeConsoleApplication.Controllers.DisplayController;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Controllers
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
            throw new ValidationException(ApplyColorToMessage(ErrorValidationFailed, Color.DarkRed));
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
                Display(exception.Message);
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
                throw new ArgumentNullException(nameof(inputArray), ApplyColorToMessage(ErrorNullCollection, Color.DarkRed));
            }
            bool collectionIsEmpty = !inputArray.Any();
            if (collectionIsEmpty)
            {
                throw new ArgumentException(ApplyColorToMessage(ErrorEmptyCollection, Color.DarkRed), nameof(inputArray));
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
                    throw new FormatException(ApplyColorToMessage(ErrorWrongInputFormat(element, currentCulture), Color.DarkRed));
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
                    throw new ArgumentException(ApplyColorToMessage(ErrorUnexpectedDateOrder(previousDate?.ToShortDateString(),
                                                                                    date.ToShortDateString()), Color.DarkRed));
                }
                previousDate = date;
            }

            return true;
        }
        #endregion
    }
}
