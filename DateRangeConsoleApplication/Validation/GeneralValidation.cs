using System;
using System.Collections.Generic;
using System.Globalization;
using DateRangeConsoleApplication.UI;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Validation
{
    internal class GeneralValidation<T, TN> where TN : IComparable<TN>
    {
        // Delegates
        private Action<IList<T>, TN> _validationAction;

        // Controllers
        internal bool ProcessInputData(IList<T> arguments, TN numberOfArguments)
        {
            _validationAction = ValidNumberOfArguments;
            _validationAction += ValidDateTimeFormat;

            return ValidationResult(_validationAction, arguments, numberOfArguments);
        }

        // Methods
        private static bool ValidationResult(Action<IList<T>, TN> validationCriteria, IList<T> arguments, TN numberOfArguments)
        {
            try
            {
                validationCriteria(arguments, numberOfArguments);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
            }

            return true;
        }

        #region Number of arguments
        private static void ValidNumberOfArguments(IList<T> arguments, TN numberOfArguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments), Utilities.DisplayInColor(ErrorNullCollection));
            }
            if (arguments.Count == 0)
            {
                throw new ArgumentException(Utilities.DisplayInColor(ErrorEmptyCollection), nameof(arguments));
            }
            if (arguments.Count.CompareTo(numberOfArguments) < 0)
            {
                throw new ArgumentException(Utilities.DisplayInColor(ErrorNotEnoughArguments(numberOfArguments)), nameof(arguments));
            }
            if (arguments.Count.CompareTo(numberOfArguments) > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arguments), Utilities.DisplayInColor(
                                                                                            ErrorToMuchArguments(numberOfArguments)));
            }
        }
        #endregion

        #region Proper date format
        private static void ValidDateTimeFormat(IList<T> arguments, TN numberOfArguments)
        {
            var culture = CultureInfo.CurrentUICulture;
            Console.WriteLine(culture.DateTimeFormat);
//            foreach (var element in arguments)
//            {
//                DateTime.TryParseExact(element, System.Globalization.CultureInfo.InvariantCulture,
//                    System.Globalization.DateTimeStyles.NoCurrentDateDefault, out element);
//            }


        }
        #endregion
    }
}
