using System;
using System.Collections.Generic;
using System.Globalization;
using DateRangeConsoleApplication.UI;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ValidationController<T, TN> where T : IComparable
    {
        // Delegates
        private delegate void ParamsAction(params object[] parameters);

        // Controllers
        internal IList<DateTime> CheckInputData(IList<T> collection, TN numberOfArguments, CultureInfo currentCulture)
        {
            // Add new validation method
            ParamsAction validationCriteria = delegate { ValidNumberOfArguments(collection, numberOfArguments); };

            // Add new validation method
            validationCriteria += delegate { ValidDateTimeFormat(collection, currentCulture); };

            // Add new validation method
            ConversionController<T, TN> converter = new ConversionController<T, TN>();
            IList<DateTime> convertedDateCollection = converter.ProcessInputData(collection, currentCulture);
            validationCriteria += delegate { CompareDateTimeValues(convertedDateCollection); };

            if (ValidationResult(validationCriteria, new object[] { }))
            {
                return convertedDateCollection;
            }
            throw new NullReferenceException(Utilities.DisplayInColor(message: ErrorValidationFailed));
        }

        // Methods
        #region Handling exceptions
        private static bool ValidationResult(ParamsAction validationCriteria, object[] parameters)
        {
            try
            {
                validationCriteria(parameters);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
            }

            return true;
        }
        #endregion

        #region Validation: Number of arguments
        private static void ValidNumberOfArguments(IList<T> collection, TN numberOfArguments)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection),
                                                Utilities.DisplayInColor(message: ErrorNullCollection));
            }
            if (collection.Count == 0)
            {
                throw new ArgumentException(Utilities.DisplayInColor(message: ErrorEmptyCollection),
                                            nameof(collection));
            }
            if (collection.Count.CompareTo(numberOfArguments) < 0)
            {
                throw new ArgumentException(Utilities.DisplayInColor(message: ErrorNotEnoughArguments(numberOfArguments)),
                                            nameof(collection));
            }
            if (collection.Count.CompareTo(numberOfArguments) > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(collection),
                                                      Utilities.DisplayInColor(message: ErrorToMuchArguments(numberOfArguments)));
            }
        }
        #endregion

        #region Validation: Proper date format
        private static void ValidDateTimeFormat(IList<T> collection, CultureInfo currentCulture)
        {
            foreach (var element in collection)
            {
                if (!TryParseExactDateTime(element, currentCulture, out DateTime date))
                {
                    throw new FormatException(Utilities.DisplayInColor(message: ErrorWrongInputFormat(element, currentCulture)));
                }
            }
        }

        /// <summary>
        /// Checks if given input is DateTime type in one of two formats (short or long) and returns parsed input
        /// </summary>
        protected static bool TryParseExactDateTime(T element, CultureInfo currentCulture, out DateTime date)
        {
            if (DateTime.TryParseExact(element.ToString(), currentCulture.DateTimeFormat.ShortDatePattern,
                currentCulture, DateTimeStyles.AssumeLocal, out date))
            {
                return true;
            }
            if (DateTime.TryParseExact(element.ToString(), currentCulture.DateTimeFormat.LongDatePattern,
                currentCulture, DateTimeStyles.AssumeLocal, out date))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Validation: Compare date objects
        private static void CompareDateTimeValues(IList<DateTime> convertedCollection)
        {
            int collectionSize = convertedCollection.Count;
            for (int i = 0; i < collectionSize; i++)
            {
                if (i != collectionSize - 1)
                {
                    if (convertedCollection[i].CompareTo(convertedCollection[i + 1]) > 0)
                    {
                        throw new ArgumentException(Utilities.DisplayInColor(message: ErrorUnexpectedDateOrder(
                                                                                convertedCollection[i].ToShortDateString(),
                                                                                convertedCollection[i + 1].ToShortDateString())));
                    }
                }
            }
        }
        #endregion
    }
}
