using System.Collections.Generic;
using DateRangeConsoleApplication.Validation;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ApplicationController<T, TN>
    {
        internal void Start(IList<T> arguments, TN validNumOfArguments)
        {
            var validation = new GeneralValidation<T, TN>();
            validation.ProcessInputData(arguments, validNumOfArguments);
        }
    }
}
