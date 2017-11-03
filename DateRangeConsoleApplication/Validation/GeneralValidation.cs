using System;
using System.Collections.Generic;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Validation
{
    internal class GeneralValidation<T, TN> where TN : IComparable<TN>
    {
        internal IList<T> ProcessInputData(IList<T> arguments, TN numberOfArguments)
        {
            try
            {
                Console.WriteLine(ValidNumberOfArguments(arguments, numberOfArguments));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.ReadKey();
            return null;
        }

        private static bool ValidNumberOfArguments(IList<T> arguments, TN numberOfArguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments), ErrorNullCollection);
            }
            if (arguments.Count == 0)
            {
                throw new ArgumentException(ErrorEmptyCollection, nameof(arguments));
            }
            if (arguments.Count.CompareTo(numberOfArguments) < 0)
            {
                throw new ArgumentException(ErrorNotEnoughArguments(numberOfArguments), nameof(arguments));
            }
            if (arguments.Count.CompareTo(numberOfArguments) > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arguments), ErrorToMuchArguments(numberOfArguments));
            }

            return true;
        }
    }
}
