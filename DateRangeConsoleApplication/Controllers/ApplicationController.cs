using System;
using System.Collections.Generic;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ApplicationController<T, TN> where T : IComparable
    {
        // Controllers
        internal void Start(IList<T> collection, TN numberOfArguments)
        {
            try
            {
                ValidationController<T, TN> validator = new ValidationController<T, TN>();
                IList<DateTime> validationResult = validator.CheckInputData(collection, numberOfArguments);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
            }
        }
    }
}
