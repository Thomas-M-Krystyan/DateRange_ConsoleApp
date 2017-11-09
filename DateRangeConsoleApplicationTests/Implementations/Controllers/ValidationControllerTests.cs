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

        private readonly string[] _invalidPolishShortDates = new string[]
        {
            "2017 05 01", "2017 05 1",  "2017 5 01",  "2017 5 1",   "2017 05 00",
            "2017 00 01", "2017 13 01", "2017 05 32", "2017 02 29", "217 05 01",
                          "2017-05-1",  "2017-5-01",  "2017-5-1",   "2017-05-00",
            "2017-00-01", "2017-13-01", "2017-05-32", "2017-02-29", "217-05-01",
            "2017.05.01", "2017.05.1",  "2017.5.01",  "2017.5.1",   "2017.05.00",
            "2017.00.01", "2017.13.01", "2017.05.32", "2017.02.29", "217.05.01",
            "2017/05/01", "2017/05/1",  "2017/5/01",  "2017/5/1",   "2017/05/00",
            "2017/00/01", "2017/13/01", "2017/05/32", "2017/02/29", "217/05/01"
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
        [Category("Invalid conversion to date")]
        public void Test_IfSingleInput_CannotBeConvertedTo_PolishShortDate_ThrowsException()
        {
            // Assert
            foreach (string date in this._invalidPolishShortDates)
            {
                string[] shortDate = new string[1] { date };
                Assert.Throws<FormatException>(() => this._validator.CheckInputArray(shortDate, this._currentCulture));
            }
        }

        [Test(Description = "From two passed inputs, first is invalid in Polish culture")]
        [Category("Invalid conversion to date")]
        public void Test_IfFromTwoInputs_First_CannotBeConvertedTo_PolishShortDate_ThrowsException()
        {
            // Assert
            foreach (string date in this._invalidPolishShortDates)
            {
                string[] shortDate = new string[2] { date, "2017-11-09" };
                Assert.Throws<FormatException>(() => this._validator.CheckInputArray(shortDate, this._currentCulture));
            }
        }

        [Test(Description = "From two passed inputs, second is invalid in Polish culture")]
        [Category("Invalid conversion to date")]
        public void Test_IfFromTwoInputs_Second_CannotBeConvertedTo_PolishShortDate_ThrowsException()
        {
            // Assert
            foreach (string date in this._invalidPolishShortDates)
            {
                string[] shortDate = new string[2] { "2017-11-09", date };
                Assert.Throws<FormatException>(() => this._validator.CheckInputArray(shortDate, this._currentCulture));
            }
        }

        [Test(Description = "Single input is valid in Polish culture")]
        [Category("Successful conversion to date")]
        public void Test_IfSingleInput_IsConvertibleTo_PolishShortDate_ReturnsDateTimeArray()
        {
            // Arrange
            const string inputString = "2017-11-09";
            string[] inputArray = new string[1] { inputString };
            DateTime[] expectedDateArray = new DateTime[] { new DateTime(2017, 11, 09) };

            // Act
            DateTime[] actualDateArray = this._validator.CheckInputArray(inputArray, this._currentCulture);

            // Assert
            Assert.AreEqual(expectedDateArray, actualDateArray);
        }

        [Test(Description = "Two inputs are valid in Polish culture")]
        [Category("Successful conversion to date")]
        public void Test_IfTwoInputs_AreConvertibleTo_PolishShortDate_ReturnsDateTimeArray()
        {
            // Arrange
            const string firstInputString = "2017-11-09";
            const string secondInputString = "2017-11-10";
            string[] inputArray = new string[2] { firstInputString, secondInputString };
            DateTime[] expectedDateArray = new DateTime[] { new DateTime(2017, 11, 09), new DateTime(2017, 11, 10) };

            // Act
            DateTime[] actualDateArray = this._validator.CheckInputArray(inputArray, this._currentCulture);

            // Assert
            Assert.AreEqual(expectedDateArray, actualDateArray);
        }

        [Test(Description = "Three inputs are valid in Polish culture")]
        [Category("Successful conversion to date")]
        public void Test_IfThreeInputs_AreConvertibleTo_PolishShortDate_ReturnsDateTimeArray()
        {
            // Arrange
            const string firstInputString = "2017-11-09";
            const string secondInputString = "2017-11-10";
            const string thirdInputString = "2017-11-11";
            string[] inputArray = new string[3] { firstInputString, secondInputString, thirdInputString };
            DateTime[] expectedDateArray = new DateTime[] { new DateTime(2017, 11, 09), new DateTime(2017, 11, 10),
                                                            new DateTime(2017, 11, 11) };
            // Act
            DateTime[] actualDateArray = this._validator.CheckInputArray(inputArray, this._currentCulture);

            // Assert
            Assert.AreEqual(expectedDateArray, actualDateArray);
        }
    }
}
