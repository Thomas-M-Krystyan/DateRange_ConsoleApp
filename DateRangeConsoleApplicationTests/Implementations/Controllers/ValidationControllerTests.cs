using System;
using System.Globalization;
using DateRangeConsoleApplication.Implementations.Controllers;
using NUnit.Framework;

namespace DateRangeConsoleApplicationTests.Implementations.Controllers
{
    [TestFixture]
    public class ValidationControllerTest
    {
        private CultureInfo _currentCulture;
        private ValidationController _validator;

        private readonly string[] _invalidPolishDates = new string[]
        {
            "2017 05 01", "2017 05 1",  "2017 5 01",  "2017 5 1",   "2017 05 00",
            "2017 00 01", "2017 13 01", "2017 05 32", "2017 02 29", "217 05 01",
                          "2017-05-1",  "2017-5-01",  "2017-5-1",   "2017-05-00",
            "2017-00-01", "2017-13-01", "2017-05-32", "2017-02-29", "217-05-01",
            "2017.05.01", "2017.05.1",  "2017.5.01",  "2017.5.1",   "2017.05.00",
            "2017.00.01", "2017.13.01", "2017.05.32", "2017.02.29", "217.05.01",
            "2017/05/01", "2017/05/1",  "2017/5/01",  "2017/5/1",   "2017/05/00",
            "2017/00/01", "2017/13/01", "2017/05/32", "2017/02/29", "217/05/01",
            "1 Listopa 2017", "1 Lis 2017", "2017 Listopad 1", "2017 1 Listopad",
            "Listopad 1 2017", "1 ListopadD 2017", "017-05-01", "17-05-01"
        };

        private readonly string[] _invalidJapaneseDates = new string[]
        {
            "2017 05 01", "2017 05 1",  "2017 5 01",  "2017 5 1",   "2017 05 00",
            "2017 00 01", "2017 13 01", "2017 05 32", "2017 02 29", "217 05 01",
            "2017-05-01", "2017-05-1",  "2017-5-01",  "2017-5-1",   "2017-05-00",
            "2017-00-01", "2017-13-01", "2017-05-32", "2017-02-29", "217-05-01",
            "2017.05.01", "2017.05.1",  "2017.5.01",  "2017.5.1",   "2017.05.00",
            "2017.00.01", "2017.13.01", "2017.05.32", "2017.02.29", "217.05.01",
                          "2017/05/1",  "2017/5/01",  "2017/5/1",   "2017/05/00",
            "2017/00/01", "2017/13/01", "2017/05/32", "2017/02/29", "217/05/01",
            "17年11月9日", "2017年111月9日", "2017年11月99日", "2017年11月9日人間"
        };

        [SetUp]
        public void Init()
        {
            this._currentCulture = new CultureInfo("pl-PL");
            this._validator = new ValidationController();
        }

        [Test(Description = "Passed collection is empty")]
        [Category("Invalid collection")]
        public void Test_IfCollectionIsEmpty_ThrowsException()
        {
            // Arrange
            string[] inputArray = new string[] { };

            // Assert
            Assert.Throws<ArgumentException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
        }

        [Test(Description = "Single passed input is invalid")]
        [Category("Invalid characters")]
        public void Test_IfSingleInput_CannotBeConvertedToDate_ThrowsException()
        {
            // Arrange
            string[] inputArray = new string[] {"!@#$%^&*"};

            // Assert
            Assert.Throws<FormatException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
        }

        [Test(Description = "From two passed inputs, first is invalid")]
        [Category("Invalid characters")]
        public void Test_IfFromTwoInputs_First_CannotBeConvertedToDate_ThrowsException()
        {
            // Arrange
            string[] inputArray = new string[] { "!@#$%^&*", "2017-11-09" };

            // Assert
            Assert.Throws<FormatException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
        }

        [Test(Description = "From two passed inputs, second is invalid")]
        [Category("Invalid characters")]
        public void Test_IfFromTwoInputs_Second_CannotBeConvertedToDate_ThrowsException()
        {
            // Arrange
            string[] inputArray = new string[] { "2017-11-09", "!@#$%^&*" };

            // Assert
            Assert.Throws<FormatException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
        }

        [Test(Description = "From two passed inputs, both are invalid")]
        [Category("Invalid characters")]
        public void Test_IfFromTwoInputs_Both_CannotBeConvertedToDate_ThrowsException()
        {
            // Arrange
            string[] inputArray = new string[] { "!@#$%^&*", "!@#$%^&*" };

            // Assert
            Assert.Throws<FormatException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
        }

        [Test(Description = "Single passed input is invalid in Polish culture")]
        [Category("Conversion to date")]
        public void Test_IfSingleInput_CannotBeConvertedTo_PolishDate_ThrowsException()
        {
            // Assert
            foreach (string date in this._invalidPolishDates)
            {
                string[] inputArray = new string[1] { date };
                Assert.Throws<FormatException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
            }
        }

        [Test(Description = "From two passed inputs, first is invalid in Polish culture")]
        [Category("Conversion to date")]
        public void Test_IfFromTwoInputs_First_CannotBeConvertedTo_PolishDate_ThrowsException()
        {
            // Assert
            foreach (string date in this._invalidPolishDates)
            {
                string[] inputArray = new string[2] { date, "2017-11-09" };
                Assert.Throws<FormatException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
            }
        }

        [Test(Description = "From two passed inputs, second is invalid in Polish culture")]
        [Category("Conversion to date")]
        public void Test_IfFromTwoInputs_Second_CannotBeConvertedTo_PolishDate_ThrowsException()
        {
            // Assert
            foreach (string date in this._invalidPolishDates)
            {
                string[] inputArray = new string[2] { "2017-11-09", date };
                Assert.Throws<FormatException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
            }
        }

        [Test(Description = "Single passed input is invalid in Japanese culture")]
        [Category("Conversion to date")]
        public void Test_IfSingleInput_CannotBeConvertedTo_JapaneseDate_ThrowsException()
        {
            // Assert
            this._currentCulture = new CultureInfo("ja-JP");
            foreach (string date in this._invalidJapaneseDates)
            {
                string[] inputArray = new string[1] { date };
                Assert.Throws<FormatException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
            }
        }

        [Test(Description = "From two passed inputs, first is invalid in Japanese culture")]
        [Category("Conversion to date")]
        public void Test_IfFromTwoInputs_First_CannotBeConvertedTo_JapaneseDate_ThrowsException()
        {
            // Assert
            this._currentCulture = new CultureInfo("ja-JP");
            foreach (string date in this._invalidJapaneseDates)
            {
                string[] inputArray = new string[2] { date, "2017/11/09" };
                Assert.Throws<FormatException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
            }
        }

        [Test(Description = "From two passed inputs, second is invalid in Japanese culture")]
        [Category("Conversion to date")]
        public void Test_IfFromTwoInputs_Second_CannotBeConvertedTo_JapaneseDate_ThrowsException()
        {
            // Assert
            this._currentCulture = new CultureInfo("ja-JP");
            foreach (string date in this._invalidJapaneseDates)
            {
                string[] inputArray = new string[2] { "2017/11/09", date };
                Assert.Throws<FormatException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
            }
        }

        [Test(Description = "Single input is valid in Polish culture")]
        [Category("Conversion to date")]
        public void Test_IfSingleInput_IsConvertibleTo_PolishDate_ReturnsDateTimeArray()
        {
            // Arrange
            const string firstInputString = "2017-11-09";
            string[] inputArray = new string[1] { firstInputString };
            DateTime firstDate = new DateTime(2017, 11, 09);
            DateTime[] expectedDateArray = new DateTime[] { firstDate };

            // Act
            DateTime[] actualDateArray = this._validator.CheckInputArray(inputArray, this._currentCulture);

            // Assert
            Assert.AreEqual(expectedDateArray, actualDateArray);
        }

        [Test(Description = "Two inputs are valid in Polish culture")]
        [Category("Conversion to date")]
        public void Test_IfTwoInputs_AreConvertibleTo_PolishDate_ReturnsDateTimeArray()
        {
            // Arrange
            const string firstInputString = "2017-11-09";
            const string secondInputString = "2017-11-10";
            string[] inputArray = new string[2] { firstInputString, secondInputString };
            DateTime firstDate = new DateTime(2017, 11, 09);
            DateTime secondDate = new DateTime(2017, 11, 10);
            DateTime[] expectedDateArray = new DateTime[] { firstDate, secondDate };

            // Act
            DateTime[] actualDateArray = this._validator.CheckInputArray(inputArray, this._currentCulture);

            // Assert
            Assert.AreEqual(expectedDateArray, actualDateArray);
        }

        [Test(Description = "Single input is valid in Japanese culture")]
        [Category("Conversion to date")]
        public void Test_IfSingleInput_IsConvertibleTo_JapaneseDate_ReturnsDateTimeArray()
        {
            // Arrange
            const string firstInputString = "2017/11/09";
            string[] inputArray = new string[1] { firstInputString };
            this._currentCulture = new CultureInfo("ja-JP");
            DateTime firstDate = new DateTime(2017, 11, 09);
            DateTime[] expectedDateArray = new DateTime[] { firstDate };

            // Act
            DateTime[] actualDateArray = this._validator.CheckInputArray(inputArray, this._currentCulture);

            // Assert
            Assert.AreEqual(expectedDateArray, actualDateArray);
        }

        [Test(Description = "Two inputs are valid in Japanese culture")]
        [Category("Conversion to date")]
        public void Test_IfTwoInputs_AreConvertibleTo_JapaneseDate_ReturnsDateTimeArray()
        {
            // Arrange
            const string firstInputString = "2017/11/09";
            const string secondInputString = "2017/11/10";
            string[] inputArray = new string[2] { firstInputString, secondInputString };
            this._currentCulture = new CultureInfo("ja-JP");
            DateTime firstDate = new DateTime(2017, 11, 09);
            DateTime secondDate = new DateTime(2017, 11, 10);
            DateTime[] expectedDateArray = new DateTime[] { firstDate, secondDate };

            // Act
            DateTime[] actualDateArray = this._validator.CheckInputArray(inputArray, this._currentCulture);

            // Assert
            Assert.AreEqual(expectedDateArray, actualDateArray);
        }

        [Test(Description = "Dates are in descenging order")]
        [Category("Dates order")]
        public void Test_IfDates_AreInDescendingOrder_ThrowsException()
        {
            // Arrange
            const string firstInputString = "2017-11-11";
            const string secondInputString = "2017-11-10";
            const string thirdInputString = "2017-11-09";
            string[] inputArray = new string[3] { firstInputString, secondInputString, thirdInputString };

            Assert.Throws<ArgumentException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
        }

        [Test(Description = "Dates are in mixed order")]
        [Category("Dates order")]
        public void Test_IfDates_AreInMixedOrder_ThrowsException()
        {
            // Arrange
            const string firstInputString = "2017-11-09";
            const string secondInputString = "2017-11-11";
            const string thirdInputString = "2017-11-10";
            string[] inputArray = new string[3] { firstInputString, secondInputString, thirdInputString };

            Assert.Throws<ArgumentException>(() => this._validator.CheckInputArray(inputArray, this._currentCulture));
        }

        [Test(Description = "Dates are in ascending order")]
        [Category("Dates order")]
        public void Test_IfDates_AreInAscendingOrder_ReturnsDateTimeArray()
        {
            // Arrange
            const string firstInputString = "2017-11-09";
            const string secondInputString = "2017-11-10";
            const string thirdInputString = "2017-11-11";
            string[] inputArray = new string[3] { firstInputString, secondInputString, thirdInputString };
            DateTime firstDate = new DateTime(2017, 11, 09);
            DateTime secondDate = new DateTime(2017, 11, 10);
            DateTime thirdDate = new DateTime(2017, 11, 11);
            DateTime[] expectedDateArray = new DateTime[] { firstDate, secondDate, thirdDate };

            // Act
            DateTime[] actualDateArray = this._validator.CheckInputArray(inputArray, this._currentCulture);

            // Assert
            Assert.AreEqual(expectedDateArray, actualDateArray);
        }

        [Test(Description = "Dates are the same")]
        [Category("Dates order")]
        public void Test_IfDates_AreTheSame_ReturnsDateTimeArray()
        {
            // Arrange
            const string firstInputString = "2017-11-10";
            const string secondInputString = "2017-11-10";
            const string thirdInputString = "2017-11-10";
            string[] inputArray = new string[3] { firstInputString, secondInputString, thirdInputString };
            DateTime firstDate = new DateTime(2017, 11, 10);
            DateTime secondDate = new DateTime(2017, 11, 10);
            DateTime thirdDate = new DateTime(2017, 11, 10);
            DateTime[] expectedDateArray = new DateTime[] { firstDate, secondDate, thirdDate };

            // Act
            DateTime[] actualDateArray = this._validator.CheckInputArray(inputArray, this._currentCulture);

            // Assert
            Assert.AreEqual(expectedDateArray, actualDateArray);
        }
    }
}
