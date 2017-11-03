using DateRangeConsoleApplication.Validation;

namespace DateRangeConsoleApplication.Controllers
{
    internal class ApplicationController<TCollection, TArgNumbers>
    {
        internal void Start(TCollection[] arguments, TArgNumbers numberOfArguments)
        {
            var validation = new GeneralValidation<TCollection, TArgNumbers>();
            validation.ProcessInputData(arguments, numberOfArguments);
        }
    }
}
