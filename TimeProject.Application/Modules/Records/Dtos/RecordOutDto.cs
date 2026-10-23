
using TimeProject.Application.Modules.Categories.Dtos;

namespace TimeProject.Application.Modules.Records.Dtos;

public class RecordOutDto
{
    public int RecordId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? ExternalLink { get; set; }

    public CategoryOutDto? Category { get; set; }
    public string? CategoryName => Category?.Name;
    public int? CategoryId { get; set; }

    public RecordResumeOutDto? Resume { get; set; }
}