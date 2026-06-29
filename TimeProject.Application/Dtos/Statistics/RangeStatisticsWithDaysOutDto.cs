namespace TimeProject.Application.Dtos.Statistics;

public class RangeStatisticsWithDaysOutDto
{
    public RangeStatisticOutDto Total { get; set; } = new();
    public IList<RangeStatisticOutDto> Days { get; set; } = [];
}