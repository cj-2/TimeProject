using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Modules.Feedbacks.Dtos;

public class PublicFeedbackDto : FeedbackDto
{
    [Required] public string Name { get; set; } = "";
    [Required] public string Email { get; set; } = "";
}