using System;
using System.Globalization;
using DateRangeConsoleApplication.Implementations.Controllers;
using DateRangeConsoleApplication.Implementations.Factory;
using DateRangeConsoleApplication.Implementations.Factory.DateRange;
using DateRangeConsoleApplication.Interfaces.Factory.DateRange;
using NUnit.Framework;

namespace DateRangeConsoleApplicationTests.Implementations.Factory
{
    [TestFixture]
    public class DateRangeFactoryTests
    {
        private CultureInfo _currentCulture;
        private ValidationController _validator;
        private DateRangeFactory _dateRange;

        [TestFixture]
        public class PolishCultureTests : DateRangeFactoryTests
        {
            [SetUp]
            public void Init()
            {
                this._currentCulture = new CultureInfo("pl-PL");
                this._validator = new ValidationController();
                this._dateRange = new DateRangeFactory(this._validator);
            }

            [Test(Description = "Single date returns DateRangeDay object")]
            [Category("Date range: Day")]
            public void Test_SingleDate_ForPolishCulture_ReturnsDateRangeDay()
            {
                // Arrange
                const string firstDate = "2017-11-09";
                string[] inputArray = new string[] { firstDate };
                Type expectedResult = typeof(DateRangeSameDay);

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.GetType());
            }

            [Test(Description = "Single date returns DateRangeDay string")]
            [Category("Date range: Day")]
            public void Test_IfSameDate_ForPolishCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017-11-09";
                string[] inputArray = new string[] { firstDate };
                const string expectedResult = "2017-11-09";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Two same dates returns DateRangeDay string")]
            [Category("Date range: Day")]
            public void Test_IfTwoSameDates_ForPolishCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017-11-09";
                const string secondDate = "2017-11-09";
                string[] inputArray = new string[] { firstDate, secondDate };
                const string expectedResult = "2017-11-09";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Two dates with the same month returns DateRangeMonth object")]
            [Category("Date range: Month")]
            public void Test_IfTwoDatesWithSameMonth_ForPolishCulture_ReturnsDateRangeMonth()
            {
                // Arrange
                const string firstDate = "2017-11-15";
                const string secondDate = "2017-11-20";
                string[] inputArray = new string[] { firstDate, secondDate };
                Type expectedResult = typeof(DateRangeSameMonth);

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.GetType());
            }

            [Test(Description = "Two dates with the same month returns DateRangeMonth string")]
            [Category("Date range: Month")]
            public void Test_IfTwoDatesWithSameMonth_ForPolishCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017-11-15";
                const string secondDate = "2017-11-20";
                string[] inputArray = new string[] { firstDate, secondDate };
                const string expectedResult = "2017-11-15—20";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Three dates with the same month returns DateRangeMonth string")]
            [Category("Date range: Month")]
            public void Test_IfThreeDatesWithSameMonth_ForPolishCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017-11-15";
                const string secondDate = "2017-11-20";
                const string thirdDate = "2017-11-25";
                string[] inputArray = new string[] { firstDate, secondDate, thirdDate };
                const string expectedResult = "2017-11-15—25";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Two dates with the same year returns DateRangeYear object")]
            [Category("Date range: Year")]
            public void Test_IfTwoDatesWithSameYear_ForPolishCulture_ReturnsDateRangeYear()
            {
                // Arrange
                const string firstDate = "2017-10-15";
                const string secondDate = "2017-11-20";
                string[] inputArray = new string[] { firstDate, secondDate };
                Type expectedResult = typeof(DateRangeSameYear);

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.GetType());
            }

            [Test(Description = "Two dates with the same year returns DateRangeYear string")]
            [Category("Date range: Year")]
            public void Test_IfTwoDatesWithSameYear_ForPolishCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017-10-15";
                const string secondDate = "2017-11-20";
                string[] inputArray = new string[] { firstDate, secondDate };
                const string expectedResult = "2017-10-15 — 11-20";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Three dates with the same year returns DateRangeYear string")]
            [Category("Date range: Year")]
            public void Test_IfThreeDatesWithSameYear_ForPolishCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017-10-15";
                const string secondDate = "2017-11-20";
                const string thirdDate = "2017-12-25";
                string[] inputArray = new string[] { firstDate, secondDate, thirdDate };
                const string expectedResult = "2017-10-15 — 12-25";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Two different dates returns DateRangeVarious object")]
            [Category("Date range: Various")]
            public void Test_IfTwoDifferentDates_ForPolishCulture_ReturnsDateRangeVarious()
            {
                // Arrange
                const string firstDate = "2017-10-15";
                const string secondDate = "2018-11-20";
                string[] inputArray = new string[] { firstDate, secondDate };
                Type expectedResult = typeof(DateRangeVarious);

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.GetType());
            }

            [Test(Description = "Two different dates returns DateRangeVarious string")]
            [Category("Date range: Various")]
            public void Test_IfTwoDifferentDates_ForPolishCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017-10-15";
                const string secondDate = "2018-11-20";
                string[] inputArray = new string[] { firstDate, secondDate };
                const string expectedResult = "2017-10-15 — 2018-11-20";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Three different dates returns DateRangeVarious string")]
            [Category("Date range: Various")]
            public void Test_IfThreeDifferentDates_ForPolishCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017-10-15";
                const string secondDate = "2018-11-20";
                const string thirdDate = "2019-12-25";
                string[] inputArray = new string[] { firstDate, secondDate, thirdDate };
                const string expectedResult = "2017-10-15 — 2019-12-25";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }
        }

        [TestFixture]
        public class AfrikaansCultureTests : DateRangeFactoryTests
        {
            [SetUp]
            public void Init()
            {
                this._currentCulture = new CultureInfo("af-ZA");
                this._validator = new ValidationController();
                this._dateRange = new DateRangeFactory(this._validator);
            }

            [Test(Description = "Single date returns DateRangeDay object")]
            [Category("Date range: Day")]
            public void Test_SingleDate_ForAfrikaansCulture_ReturnsDateRangeDay()
            {
                // Arrange
                const string firstDate = "2017/11/09";
                string[] inputArray = new string[] { firstDate };
                Type expectedResult = typeof(DateRangeSameDay);

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.GetType());
            }

            [Test(Description = "Single date returns DateRangeDay string")]
            [Category("Date range: Day")]
            public void Test_IfSameDate_ForAfrikaansCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017/11/09";
                string[] inputArray = new string[] { firstDate };
                const string expectedResult = "2017/11/09";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Two same dates returns DateRangeDay string")]
            [Category("Date range: Day")]
            public void Test_IfTwoSameDates_ForAfrikaansCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017/11/09";
                const string secondDate = "2017/11/09";
                string[] inputArray = new string[] { firstDate, secondDate };
                const string expectedResult = "2017/11/09";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Two dates with the same month returns DateRangeMonth object")]
            [Category("Date range: Month")]
            public void Test_IfTwoDatesWithSameMonth_ForAfrikaansCulture_ReturnsDateRangeMonth()
            {
                // Arrange
                const string firstDate = "2017/11/15";
                const string secondDate = "2017/11/20";
                string[] inputArray = new string[] { firstDate, secondDate };
                Type expectedResult = typeof(DateRangeSameMonth);

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.GetType());
            }

            [Test(Description = "Two dates with the same month returns DateRangeMonth string")]
            [Category("Date range: Month")]
            public void Test_IfTwoDatesWithSameMonth_ForAfrikaansCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017/11/15";
                const string secondDate = "2017/11/20";
                string[] inputArray = new string[] { firstDate, secondDate };
                const string expectedResult = "2017/11/15—20";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Three dates with the same month returns DateRangeMonth string")]
            [Category("Date range: Month")]
            public void Test_IfThreeDatesWithSameMonth_ForAfrikaansCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017/11/15";
                const string secondDate = "2017/11/20";
                const string thirdDate = "2017/11/25";
                string[] inputArray = new string[] { firstDate, secondDate, thirdDate };
                const string expectedResult = "2017/11/15—25";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Two dates with the same year returns DateRangeYear object")]
            [Category("Date range: Year")]
            public void Test_IfTwoDatesWithSameYear_ForAfrikaansCulture_ReturnsDateRangeYear()
            {
                // Arrange
                const string firstDate = "2017/10/15";
                const string secondDate = "2017/11/20";
                string[] inputArray = new string[] { firstDate, secondDate };
                Type expectedResult = typeof(DateRangeSameYear);

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.GetType());
            }

            [Test(Description = "Two dates with the same year returns DateRangeYear string")]
            [Category("Date range: Year")]
            public void Test_IfTwoDatesWithSameYear_ForAfrikaansCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017/10/15";
                const string secondDate = "2017/11/20";
                string[] inputArray = new string[] { firstDate, secondDate };
                const string expectedResult = "2017/10/15 — 11/20";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Three dates with the same year returns DateRangeYear string")]
            [Category("Date range: Year")]
            public void Test_IfThreeDatesWithSameYear_ForAfrikaansCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017/10/15";
                const string secondDate = "2017/11/20";
                const string thirdDate = "2017/12/25";
                string[] inputArray = new string[] { firstDate, secondDate, thirdDate };
                const string expectedResult = "2017/10/15 — 12/25";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Two different dates returns DateRangeVarious object")]
            [Category("Date range: Various")]
            public void Test_IfTwoDifferentDates_ForAfrikaansCulture_ReturnsDateRangeVarious()
            {
                // Arrange
                const string firstDate = "2017/10/15";
                const string secondDate = "2018/11/20";
                string[] inputArray = new string[] { firstDate, secondDate };
                Type expectedResult = typeof(DateRangeVarious);

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.GetType());
            }

            [Test(Description = "Two different dates returns DateRangeVarious string")]
            [Category("Date range: Various")]
            public void Test_IfTwoDifferentDates_ForAfrikaansCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017/10/15";
                const string secondDate = "2018/11/20";
                string[] inputArray = new string[] { firstDate, secondDate };
                const string expectedResult = "2017/10/15 — 2018/11/20";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }

            [Test(Description = "Three different dates returns DateRangeVarious string")]
            [Category("Date range: Various")]
            public void Test_IfThreeDifferentDates_ForAfrikaansCulture_ReturnsProperToStringRange()
            {
                // Arrange
                const string firstDate = "2017/10/15";
                const string secondDate = "2018/11/20";
                const string thirdDate = "2019/12/25";
                string[] inputArray = new string[] { firstDate, secondDate, thirdDate };
                const string expectedResult = "2017/10/15 — 2019/12/25";

                // Act
                IDateRange actualResult = this._dateRange.From(inputArray, this._currentCulture);

                // Assert
                Assert.AreEqual(expectedResult, actualResult.ToString());
            }
        }
    }
}
