using System;
using System.Globalization;

namespace DateRangeConsoleApplication.Interfaces.Validation
{
    public interface IValidationController
    {
       DateTime[] CheckInputArray(string[] stringArray, CultureInfo currentCulture);
    }
}
