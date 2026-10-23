using System.ComponentModel.DataAnnotations;

namespace TimeProject.Application.Modules.Feedbacks.Dtos;

public class FeedbackDto
{
    [Required] public string Message { get; set; } = "";
}