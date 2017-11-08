using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DateRangeConsoleApplication.Implementations.Factory;
using DateRangeConsoleApplication.Interfaces.Factory.DateRange;
using static DateRangeConsoleApplication.UI.Messages.EnglishMessages;

namespace DateRangeConsoleApplication.Implementations.Controllers
{
    internal class ApplicationController
    {
        internal void Start(string[] inputArray)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            ValidationController validator = new ValidationController();

            try
            {
                DateRangeFactory dateRange = new DateRangeFactory(validator);
                IDateRange result = dateRange.From(inputArray, currentCulture);

                DisplayController.Display(DisplayController.SetMessageColor(MonitOperationSucceed, 
                                          DisplayController.Color.DarkGreen));
                DisplayController.Display(result.ToString());
            }
            catch (ValidationException exception)
            {
                DisplayController.Display(exception.Message);
            }
            Console.ReadKey();
        }
    }
}
