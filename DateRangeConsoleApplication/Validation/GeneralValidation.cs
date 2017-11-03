using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DateRangeConsoleApplication.UI;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Validation
{
    internal class GeneralValidation<T, TN> where TN : IComparable<TN>
    {
        // Methods
        internal IList<T> ProcessInputData(IList<T> arguments, TN numberOfArguments)
        {
            IList<T> result = new Collection<T>();

            try
            {
                ValidNumberOfArguments(arguments, numberOfArguments);
                ValidDateTimeFormat(arguments);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
            }

            return result;
        }

        #region Number of arguments
        private static bool ValidNumberOfArguments(IList<T> arguments, TN numberOfArguments)
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

            return true;
        }
        #endregion

        #region Proper data format
        private static void ValidDateTimeFormat(IList<T> arguments)
        {
            foreach (var element in arguments)
            {
                Console.WriteLine(element);
            }
        }
        #endregion
    }
}
