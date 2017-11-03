namespace DateRangeConsoleApplication.UI.Messages
{
    internal static class EnglishMessages
    {
        // Error Messages
        internal const string ErrorNullCollection = "There is no collection!";
        internal const string ErrorEmptyCollection = "Collection cannot be empty!";
        
        internal static string ErrorNotEnoughArguments(object value)
        {
            return $"Collection has less than {value} arguments";
        }

        internal static string ErrorToMuchArguments(object value)
        {
            return $"Collection has more than {value} arguments";
        }

        internal const string ErrorWrongMessageColor = "There is no such message color!";
    }
}
