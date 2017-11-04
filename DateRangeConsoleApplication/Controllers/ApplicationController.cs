using System;
using System.Collections.Generic;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ApplicationController<T, TN> where T : IComparable
    {
        // Controllers
        internal void Start(IList<T> collection, TN numberOfArguments)
        {
            ValidationController<T, TN> validator = new ValidationController<T, TN>();
            if (validator.CheckInputData(collection, numberOfArguments))
            {
                
            }
        }
    }
}
