using System;
using System.Globalization;

namespace DateRangeConsoleApplication.UI.Messages
{
    internal static class EnglishMessages
    {
        // Error Messages
        internal const string ErrorNullCollection = "ERROR: There is no collection!";
        internal const string ErrorEmptyCollection = "ERROR: Collection cannot be empty!";
        
        internal static string ErrorNotEnoughArguments(object numberOfArguments)
        {
            return $"ERROR: Collection has less than {numberOfArguments} arguments";
        }

        internal static string ErrorToMuchArguments(object numberOfArguments)
        {
            return $"ERROR: Collection has more than {numberOfArguments} arguments";
        }

        internal const string ErrorWrongMessageColor = "ERROR: There is no such message color!";

        internal static string ErrorInputNotConvertible(object inputValue)
        {
            Type dateTimeType = typeof(DateTime);

            return $"ERROR: Input \"{inputValue}\" isn't convertible to {dateTimeType} format\n" +
                   $"(possible: incorrect data type, misspellings, or given date does not exist)!";
        }

        internal static string ErrorWrongInputFormat(object inputValue, CultureInfo currentCulture)
        {
            DateTime date = DateTime.Now;

            Type dateTimeType = typeof(DateTime);
            string cultureDateSeparator = currentCulture.DateTimeFormat.DateSeparator;
            string localCultureName = currentCulture.DisplayName;
            string englishCultureName = currentCulture.EnglishName;
            string exampleShortDateFormat = date.ToString("d", currentCulture);
            string exampleLongDateFormat = date.ToString("D", currentCulture);

            return $"ERROR: Input \"{inputValue}\" cannot be converted to local {dateTimeType} format\n" +
                   $"(e.g. you given invalid data type or use wrong date separator instead of \"{cultureDateSeparator}\")\n" +
                   $"or given date does not exist (e.g. wrong day, month, or year is out of range)!\n\n" +

                   $"Your system language is:\n" +
                   $"\"{localCultureName} / {englishCultureName}\"\n\n" +

                   $"Acceptable date formats are:\n" +
                   $"\"{exampleShortDateFormat}\" (short) or \"{exampleLongDateFormat}\" (long)";
        }
    }
}
