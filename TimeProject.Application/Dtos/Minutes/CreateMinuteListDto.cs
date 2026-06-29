using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Minutes;

public class CreateMinuteListDto
{
    public int? CategoryId { get; set; }
    public int? RecordId { get; set; }
    [Required] public DateTimeOffset Date { get; set; }
    [Required] public List<int> Minutes { get; set; } = [];
}