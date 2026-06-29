using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Dtos.Feedbacks;

public class PublicFeedbackDto : FeedbackDto
{
    [Required] public string Name { get; set; } = "";
    [Required] public string Email { get; set; } = "";
}