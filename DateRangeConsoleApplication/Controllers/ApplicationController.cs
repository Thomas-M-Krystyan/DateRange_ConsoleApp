using System;
using System.Collections.Generic;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ApplicationController<T, TN> where T : IComparable
    {
        // Controllers
        internal void Start(IList<T> collection, TN numberOfArguments)
        {
            var validation = new ValidationController<T, TN>();
            if (validation.CheckInputData(collection, numberOfArguments))
            {
                
            }
        }
    }
}
