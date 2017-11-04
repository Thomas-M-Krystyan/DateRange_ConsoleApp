using System;
using System.Collections.Generic;
using System.Globalization;
using DateRangeConsoleApplication.UI;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Validation
{
    internal class GeneralValidation<T, TN> where TN : IComparable
    {
        // Constants
        private const string DateTimeIsShort = "short";
        private const string DateTimeIsLong = "long";

        // Delegates
        private delegate void ParamsAction(params object[] arguments);

        // Controllers
        internal bool ProcessInputData(IList<T> arguments, TN numberOfArguments)
        {
            ParamsAction validationCriteria = delegate { ValidNumberOfArguments(arguments, numberOfArguments); };
            validationCriteria += delegate { ValidDateTimeFormat(arguments); };

            return ValidationResult(validationCriteria, new object[]{ arguments, numberOfArguments });
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
        private static void ValidNumberOfArguments(IList<T> arguments, TN numberOfArguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments),
                                                Utilities.DisplayInColor(message: ErrorNullCollection));
            }
            if (arguments.Count == 0)
            {
                throw new ArgumentException(Utilities.DisplayInColor(message: ErrorEmptyCollection),
                                            nameof(arguments));
            }
            if (arguments.Count.CompareTo(numberOfArguments) < 0)
            {
                throw new ArgumentException(Utilities.DisplayInColor(message: ErrorNotEnoughArguments(numberOfArguments)),
                                            nameof(arguments));
            }
            if (arguments.Count.CompareTo(numberOfArguments) > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arguments),
                                                      Utilities.DisplayInColor(message: ErrorToMuchArguments(numberOfArguments)));
            }
        }
        #endregion

        #region Validation: Proper date format
        private static void ValidDateTimeFormat(IList<T> arguments)
        {
            CultureInfo currentCulture = CultureInfo.CurrentUICulture;

            DateTime date = new DateTime();
            foreach (var element in arguments)
            {
                if (RecogniseDateFormat(element, currentCulture, out date) == string.Empty)
                {
                    throw new FormatException(Utilities.DisplayInColor(message: ErrorWrongInputFormat(element, currentCulture)));
                }
            }
        }

        private static string RecogniseDateFormat(T element, CultureInfo currentCulture, out DateTime date)
        {
            if (DateTime.TryParseExact(element.ToString(), currentCulture.DateTimeFormat.ShortDatePattern,
                currentCulture, DateTimeStyles.AssumeLocal, out date))
            {
                return DateTimeIsShort;
            }
            if (DateTime.TryParseExact(element.ToString(), currentCulture.DateTimeFormat.LongDatePattern,
                currentCulture, DateTimeStyles.AssumeLocal, out date))
            {
                return DateTimeIsLong;
            }

            return string.Empty;
        }
        #endregion

        private static IList<T> ParseToSpecificCollection(IList<T> collection)
        {
            IList<T> parsedCollection;

            if (collection.IsReadOnly)
            {
                parsedCollection = new T[collection.Count];
            }
            else
            {
                parsedCollection = new List<T>();
            }

            return parsedCollection;
        }
    }
}
