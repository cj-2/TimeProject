namespace TimeProject.Application.Modules.Minutes.Dtos;

public class CreateMinuteDto
{
    public int RecordId { get; set; }
    public DateTime Date { get; set; }
    public int Minutes { get; set; }
}