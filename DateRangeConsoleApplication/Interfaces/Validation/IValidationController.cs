using System;
using System.Globalization;

namespace DateRangeConsoleApplication.Interfaces.Validation
{
    internal interface IValidationController
    {
       DateTime[] CheckInputArray(string[] stringArray, CultureInfo currentCulture);
    }
}
