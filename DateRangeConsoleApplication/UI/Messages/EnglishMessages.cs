using System;
using System.Globalization;

namespace DateRangeConsoleApplication.UI.Messages
{
    internal static class EnglishMessages
    {
        // Error Messages
        internal const string ErrorNullCollection = "ERROR: There is no collection!";
        internal const string ErrorEmptyCollection = "ERROR: Input cannot be empty!";
        internal const string ErrorWrongMessageColor = "ERROR: There is no such message color!";
        internal const string ErrorValidationFailed = "\nERROR: Validation failed. The program has stopped working!";
        internal const string ErrorInvalidFormatStrategy = "There is no such format option!";

        internal static string ErrorWrongInputFormat(object inputValue, CultureInfo currentCulture)
        {
            DateTime date = DateTime.Now;

            Type dateType = typeof(DateTime);
            string cultureDateSeparator = currentCulture.DateTimeFormat.DateSeparator;
            string cultureTimeSeparator = currentCulture.DateTimeFormat.TimeSeparator;
            string localCultureName = currentCulture.DisplayName;
            string englishCultureName = currentCulture.EnglishName;
            string exampleShortDateFormat = date.ToString("d", currentCulture);
            string exampleLongDateFormat = date.ToString("D", currentCulture);

            return $"ERROR: Your input \"{inputValue}\"\n" +
                   $"cannot be converted to known date format or style of {dateType} type!\n" +
                   $"(Possible: invalid input type, misspellings, illegal datetime separators,\n" +
                   $"or given date does not exist - e.g. day, month, or year are out of range)\n\n" +

                   $"Your system language is:\n" +
                   $"\"{localCultureName} / {englishCultureName}\"\n\n" +

                   $"Suggested date formats are:\n" +
                   $"\"{exampleShortDateFormat}\" (short) or \"{exampleLongDateFormat}\" (long)\n\n" +
                   
                   $"Suggested separators are:\n" +
                   $"\"{cultureDateSeparator}\" (for date) or \"{cultureTimeSeparator}\" (for time)";
        }

        internal static string ErrorUnexpectedDateOrder(object previousDate, object nextDate)
        {
            return $"ERROR: The previous date \"{previousDate}\" cannot be later\n" +
                   $"\tthan the farther one \"{nextDate}\"!";
        }
    }
}
