using System;
using System.Globalization;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ApplicationController
    {
        internal void Start(string[] inputArray)
        {
            try
            {
                CultureInfo currentCulture = CultureInfo.CurrentUICulture;
                currentCulture = new CultureInfo("en-US");

                ValidationController validator = new ValidationController();
                DateTime[] validationResult = validator.CheckInputData(inputArray, currentCulture);

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
