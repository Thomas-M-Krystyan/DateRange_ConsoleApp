using System;
using System.Collections.Generic;

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
                throw new ArgumentNullException($"There is no collection!");
            }
            if (arguments.Count == 0)
            {
                throw new ArgumentException($"Collection \"{nameof(arguments)}\" cannot be empty!");
            }
            if (arguments.Count.CompareTo(numberOfArguments) < 0)
            {
                throw new ArgumentException($"Collection \"{nameof(arguments)}\" has less than {numberOfArguments} arguments");
            }
            if (arguments.Count.CompareTo(numberOfArguments) > 0)
            {
                throw new ArgumentOutOfRangeException($"Collection \"{nameof(arguments)}\" has more than {numberOfArguments} arguments");
            }

            return true;
        }
    }
}
