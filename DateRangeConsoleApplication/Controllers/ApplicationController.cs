using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ApplicationController<T, TN> where T : IComparable
    {
        // Controllers
        internal void Start(IList<T> collection, TN numberOfArguments)
        {
            try
            {
                CultureInfo currentCulture = CultureInfo.CurrentUICulture;

                // Validation
                ValidationController<T, TN> validator = new ValidationController<T, TN>();
                IList<DateTime> validationResult = validator.CheckInputData(collection, numberOfArguments, currentCulture);

                // Generate range
                DateRangeController ranger = new DateRangeController();
                ranger.AnalyzeData(validationResult, currentCulture);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
            }
        }
    }
}
