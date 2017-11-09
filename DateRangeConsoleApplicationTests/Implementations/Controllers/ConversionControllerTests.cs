using System;
using System.Globalization;
using DateRangeConsoleApplication.Implementations.Controllers;
using NUnit.Framework;

namespace DateRangeConsoleApplicationTests.Implementations.Controllers
{
    [TestFixture]
    public class ConversionControllerTest
    {
        [Test(Description = "Test if passed collection is not null")]
        public void Test_IfCollectionIsNull_ThrowsException()
        {
            // Arrange
            const string[] inputArray = null;
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            ConversionController converted = new ConversionController(currentCulture);

            // Assert
            Assert.Throws<ArgumentNullException>(() => converted.ProcessInputArray(inputArray));
        }
    }
}
