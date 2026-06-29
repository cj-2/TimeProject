namespace TimeProject.Application.Dtos.Records;

public class CreatePeriodDto
{
    public int RecordId { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
}