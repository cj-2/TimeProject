using TimeProject.Application.Dtos.Feedbacks;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Feedbacks;

public interface ISendFeedbackUseCase
{
    ICustomResult<bool> Handle(FeedbackDto dto, string name, string email, bool isVerified);
}