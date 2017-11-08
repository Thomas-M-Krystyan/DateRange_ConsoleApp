using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using DateRangeConsoleApplication.Interfaces.Validation;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Implementations.Controllers
{
    internal class ValidationController : IValidationController
    {
        private Predicate<string[]> _validationCriteriaPredicate;

        public DateTime[] CheckInputArray(string[] inputArray, CultureInfo currentCulture)
        {
            this._validationCriteriaPredicate = stringArray => IsCollectionValid(inputArray);
            this._validationCriteriaPredicate += stringArray => IsInputValid(inputArray, currentCulture);

            ConversionController converter = new ConversionController(currentCulture);
            DateTime[] dateArray = converter.ProcessInputArray(inputArray);

            this._validationCriteriaPredicate += stringArray => IsDatesOrderAscending(dateArray);

            bool isValid = IsEntireValidationSucceed(_validationCriteriaPredicate, inputArray);
            if (!isValid)
            {
                throw new ValidationException(DisplayController.SetMessageColor(ErrorValidationFailed, DisplayController.Color.DarkRed));
            }

            return dateArray;
        }

        #region Handling validation exceptions
        private bool IsEntireValidationSucceed(Predicate<string[]> validationCriteriaPredicate, string[] inputArray)
        {
            try
            {
                validationCriteriaPredicate(inputArray);
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
        private bool IsCollectionValid(string[] inputArray)
        {
            bool collectionNotExist = Equals(inputArray, null);
            if (collectionNotExist)
            {
                throw new ArgumentNullException(nameof(inputArray),
                                                DisplayController.SetMessageColor(ErrorNullCollection,
                                                DisplayController.Color.DarkRed));
            }
            bool collectionIsEmpty = !inputArray.Any();
            if (collectionIsEmpty)
            {
                throw new ArgumentException(DisplayController.SetMessageColor(ErrorEmptyCollection, 
                                            DisplayController.Color.DarkRed),
                                            nameof(inputArray));
            }

            return true;
        }
        #endregion

        #region Validation: Proper date format
        private bool IsInputValid(IEnumerable<string> inputArray, CultureInfo currentCulture)
        {
            foreach (var element in inputArray)
            {
                bool canInputBeParsedToDate = TryParseToDate(element, currentCulture, out DateTime date);
                if (!canInputBeParsedToDate)
                {
                    throw new FormatException(DisplayController.SetMessageColor(ErrorWrongInputFormat(element, currentCulture),
                                              DisplayController.Color.DarkRed));
                }
            }

            return true;
        }

        internal static bool TryParseToDate(string element, CultureInfo currentCulture, out DateTime date)
        {
            return DateTime.TryParse(element, currentCulture, DateTimeStyles.AssumeLocal, out date);
        }
        #endregion

        #region Validation: Compare date objects
        private bool IsDatesOrderAscending(IEnumerable<DateTime> dateArray)
        {
            DateTime? previousDate = null;
            foreach (var date in dateArray)
            {
                bool previousDateIsLaterThanNextDate = previousDate > date;
                if (previousDateIsLaterThanNextDate)
                {
                    throw new ArgumentException(DisplayController.SetMessageColor(
                                                ErrorUnexpectedDateOrder(previousDate?.ToShortDateString(),
                                                date.ToShortDateString()), DisplayController.Color.DarkRed));
                }
                previousDate = date;
            }

            return true;
        }
        #endregion
    }
}
