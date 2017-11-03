using System;
using System.Collections.Generic;

namespace DateRangeConsoleApplication.Validation
{
    internal class GeneralValidation<T, TN>
    {
        internal IList<T> ProcessInputData(IList<T> arguments, TN validNumOfArguments)
        {


            return null;
        }

        private bool NumberOfArgumentsValidation(IList<T> arguments, TN validNumOfArguments)
        {
            if (arguments == null)
            {
                Console.WriteLine("ERROR: Null!");
            }
            else if (arguments.Count < 2)
            {
                Console.WriteLine("ERROR: Less than two elements!");
            }
            else if (arguments.Count > 1 && arguments.Count < 3)
            {
                Console.WriteLine("OK: Two elements");
                foreach (var element in arguments)
                {
                    Console.WriteLine(element);
                }
            }
            else
            {
                Console.WriteLine("ERROR: More than two elements!");
            }
            Console.ReadLine();

            return false;
        }
    }
}
