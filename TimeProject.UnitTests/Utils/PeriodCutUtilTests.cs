using FluentAssertions;
using TimeProject.Application.Utils;
using TimeProject.Domain.Entities;

namespace TimeProject.UnitTests.Utils;

public class PeriodCutUtilTests
{
    [Fact]
    public void Given_ListWithDatesBiggerOrSmallerThenRange_Then_Should_CutToRange()
    {
        #region Arrange

        var periodCutUtil = new PeriodCutUtil();

        var initDate = DateTime.Today;
        var endDate = DateTime.Today.AddDays(1);

        var list = new List<Period>
        {
            // start is before then initDate
            new()
            {
                Start = initDate.AddHours(-1),
                End = initDate.AddHours(2)
            },
            // end is after then endDate
            new()
            {
                Start = endDate.AddHours(-1),
                End = endDate.AddHours(1)
            },
            new()
            {
                Start = initDate.AddHours(2),
                End = initDate.AddHours(2).AddMinutes(15)
            },
            new()
            {
                Start = initDate.AddHours(3).AddMinutes(25),
                End = initDate.AddHours(3).AddMinutes(45)
            },
            // start and end is before then initDate (should be removed)
            new()
            {
                Start = initDate.AddHours(-5),
                End = initDate.AddHours(-4)
            }
        };

        #endregion Arrange

        #region Act

        var result = periodCutUtil.Handle(list, initDate, endDate).ToList();

        #endregion

        #region Assert

        result[0].Start.Should().Be(initDate);
        result[1].End.Should().BeBefore(endDate);
        result.Should().HaveCount(4);

        #endregion
    }
}