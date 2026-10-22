using TimeProject.Domain.Entities;

namespace TimeProject.Application.UseCases.Minutes.Dtos;

public class CreateMinuteByListOutDto
{
    public DateTimeOffset Date { get; set; }
    public int Total { get; set; }

    public static CreateMinuteByListOutDto Map(Minute minute)
    {
        return new CreateMinuteByListOutDto
        {
            Date = minute.Date,
            Total = minute.Total
        };
    }
}