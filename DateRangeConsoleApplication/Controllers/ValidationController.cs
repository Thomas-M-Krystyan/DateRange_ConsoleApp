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
        internal bool CheckInputData(IList<T> collection, TN numberOfArguments)
        {
            CultureInfo currentCulture = CultureInfo.CurrentUICulture;

            ParamsAction validationCriteria = delegate { ValidNumberOfArguments(collection, numberOfArguments); };
                         validationCriteria += delegate { ValidDateTimeFormat(collection, currentCulture); };

            if (ValidationResult(validationCriteria, new object[]{}))
            {
                ConversionController<T, TN> conversion = new ConversionController<T, TN>();
                IList<T> converteData = conversion.ProcessInputData(collection, currentCulture);
            }
            return true;
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

        #region Validation: Compare dates arguments
        private static void CompareDateTimeValues(IList<T> collection)
        {
            int collectionSize = collection.Count - 1;
            for (int i = 0; i < collectionSize; i++)
            {
                if (i != collectionSize)
                {
                    if (collection[i].CompareTo(collection[i + 1]) > 0)
                    {
                        
                    }
                }
            }
        }
        #endregion
    }
}
