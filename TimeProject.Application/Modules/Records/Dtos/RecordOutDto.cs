using TimeProject.Application.Modules.Categories.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Records.Dtos;

public class RecordOutDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? ExternalLink { get; set; }
    public string? Category { get; set; }

    public RecordResumeOutDto? Resume { get; set; }

    public static RecordOutDto Map(Record record)
    {
        return new RecordOutDto
        {
            Name = record.Name,
            Description = record.Description,
            Code = record.Code,
            ExternalLink = record.ExternalLink,
            Category = record.Category?.Name ?? "",
            Resume = record.Resume is null ? null : RecordResumeOutDto.Map(record.Resume)
        };
    }
}