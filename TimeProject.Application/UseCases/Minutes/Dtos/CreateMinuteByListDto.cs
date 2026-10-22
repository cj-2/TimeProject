using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.UseCases.Minutes.Dtos;

public class CreateMinuteByListDto
{
    public int RecordId { get; set; }
    [Required] public DateTimeOffset Date { get; set; }
    [Required] public List<int> Minutes { get; set; } = [];
}