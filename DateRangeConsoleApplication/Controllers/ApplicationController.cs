using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ApplicationController
    {
        // Controllers
        internal void Start(IList<string> collection)
        {
            try
            {
                CultureInfo currentCulture = CultureInfo.CurrentUICulture;

                // Validation
                ValidationController validator = new ValidationController();
                IList<DateTime> validationResult = validator.CheckInputData(collection, currentCulture);

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
