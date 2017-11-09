using System;
using System.Globalization;
using DateRangeConsoleApplication.Implementations.Controllers;
using NUnit.Framework;

namespace DateRangeConsoleApplicationTests.Implementations.Controllers
{
    [TestFixture]
    public class ValidationControllerTest
    {
        [Test(Description = "Test if passed collection is empty")]
        public void Test_IfCollectionIsEmpty_ThrowsException()
        {
            // Arrange
            string[] inputArray = new string[] { };
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            ValidationController validator = new ValidationController();

            // Assert
            Assert.Throws<ArgumentException>(() => validator.CheckInputArray(inputArray, currentCulture));
        }
    }
}
