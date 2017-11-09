using System;
using System.Globalization;
using DateRangeConsoleApplication.Implementations.Controllers;
using NUnit.Framework;

namespace DateRangeConsoleApplicationTests.Implementations.Controllers
{
    [TestFixture]
    public class ValidationControllerTest
    {
        private CultureInfo _currentCulture = CultureInfo.CurrentCulture;

        [SetUp]
        public void Init()
        {
            this._currentCulture = CultureInfo.CurrentCulture;
        }

        [Test(Description = "Test if passed collection is empty")]
        public void Test_IfCollectionIsEmpty_ThrowsException()
        {
            // Arrange
            string[] inputArray = new string[] { };
            ValidationController validator = new ValidationController();

            // Assert
            Assert.Throws<ArgumentException>(() => validator.CheckInputArray(inputArray, this._currentCulture));
        }

        [Test(Description = "Test if single passed input is invalid")]
        public void Test_IfSingleInputCannotBeConvertedToDate_ThrowsException()
        {
            // Arrange
            string[] inputArray = new string[] {"!@#$%^&*"};
            ValidationController validator = new ValidationController();

            // Assert
            Assert.Throws<FormatException>(() => validator.CheckInputArray(inputArray, this._currentCulture));
        }
    }
}
