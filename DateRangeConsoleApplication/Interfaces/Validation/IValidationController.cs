using System;

namespace DateRangeConsoleApplication.Interfaces.Validation
{
    internal interface IValidationController
    {
       DateTime[] CheckInputArray(string[] arguments);
    }
}
