using System;
using System.Globalization;

namespace DateRangeConsoleApplication.UI.Messages
{
    internal static class EnglishMessages
    {
        // Error Messages
        internal const string ErrorNullCollection = "There is no collection!";
        internal const string ErrorEmptyCollection = "Collection cannot be empty!";
        
        internal static string ErrorNotEnoughArguments(object numberOfArguments)
        {
            return $"Collection has less than {numberOfArguments} arguments";
        }

        internal static string ErrorToMuchArguments(object numberOfArguments)
        {
            return $"Collection has more than {numberOfArguments} arguments";
        }

        internal const string ErrorWrongMessageColor = "There is no such message color!";

        internal static string ErrorWrongInputFormat(object inputValue, CultureInfo currentCulture)
        {
            DateTime date = DateTime.Now;

            Type dateTimeType = typeof(DateTime);
            string cultureDateSeparator = currentCulture.DateTimeFormat.DateSeparator;
            string localCultureName = currentCulture.DisplayName;
            string englishCultureName = currentCulture.EnglishName;
            string exampleShortDateFormat = date.ToString("d", currentCulture);
            string exampleLongDateFormat = date.ToString("D", currentCulture);

            return $"Your input \"{inputValue}\" cannot be strictly converted to {dateTimeType} format\n" +
                   $"(e.g. you give invalid data type or use wrong date separator, instead of \"{cultureDateSeparator}\")\n" +
                   $"or given date does not exist (e.g. wrong day, month, or year is out of range)!\n\n" +

                   $"Your system language is:\n" +
                   $"\"{localCultureName} / {englishCultureName}\"\n\n" +

                   $"Acceptable date formats are:\n" +
                   $"\"{exampleShortDateFormat}\" (short) or \"{exampleLongDateFormat}\" (long)";
        }
    }
}
