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
                DateTime[] validationResult = validator.CheckInputArray(inputArray, currentCulture);

                DateRangeController ranger = new DateRangeController();
                string result = ranger.AnalyzeData(validationResult, currentCulture);
                DisplayController.Display(result);
                Console.ReadKey();
            }
            catch (Exception exception)
            {
                DisplayController.Display(exception.Message);
                Console.ReadKey();
            }
        }
    }
}
