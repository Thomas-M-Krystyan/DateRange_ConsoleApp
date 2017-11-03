using System;

namespace DateRangeConsoleApplication.Validation
{
    internal class GeneralValidation<TCollection, TArgNumbers>
    {
        internal TCollection[] ProcessInputData(TCollection[] arguments, TArgNumbers numberOfArguments)
        {


            return null;
        }

        private bool NumberOfArgumentsValidation(TCollection[] arguments, TArgNumbers numberOfArguments)
        {
            if (arguments == null)
            {
                Console.WriteLine("ERROR: Null!");
            }
            else if (arguments.Length < 2)
            {
                Console.WriteLine("ERROR: Less than two elements!");
            }
            else if (arguments.Length > 1 && arguments.Length < 3)
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
