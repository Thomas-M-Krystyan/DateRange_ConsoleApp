using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DateRangeConsoleApplication.UI;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Validation
{
    internal class GeneralValidation<T, TN> where TN : IComparable<TN>
    {
        // Constants
        private const string ErrorMessageColor = "red";

        // Methods
        internal IList<T> ProcessInputData(IList<T> arguments, TN numberOfArguments)
        {
            IList<T> result = new Collection<T>();

            try
            {
                ValidNumberOfArguments(arguments, numberOfArguments);
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
                throw new ArgumentNullException(nameof(arguments),
                                                Utilities.DisplayColor(message: ErrorNullCollection, color: ErrorMessageColor));
            }
            if (arguments.Count == 0)
            {
                throw new ArgumentException(Utilities.DisplayColor(message: ErrorEmptyCollection, color: ErrorMessageColor),                                     nameof(arguments));
            }
            if (arguments.Count.CompareTo(numberOfArguments) < 0)
            {
                throw new ArgumentException(Utilities.DisplayColor(message: ErrorNotEnoughArguments(numberOfArguments),
                                                                   color: ErrorMessageColor), nameof(arguments));
            }
            if (arguments.Count.CompareTo(numberOfArguments) > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arguments), 
                                                      Utilities.DisplayColor(message: ErrorToMuchArguments(numberOfArguments),
                                                                             color: ErrorMessageColor));
            }

            return true;
        }
        #endregion
    }
}
