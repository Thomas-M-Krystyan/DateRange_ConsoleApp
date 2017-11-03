using System;
using System.Collections.Generic;
using DateRangeConsoleApplication.Validation;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ApplicationController<T, TN> where TN : IComparable<TN>
    {
        // Methods
        internal void Start(IList<T> arguments, TN numberOfArguments)
        {
            var validation = new GeneralValidation<T, TN>();
            if (validation.ProcessInputData(arguments, numberOfArguments))
            {
                
            }
        }
    }
}
