using TimeProject.Application.Modules.Feedbacks.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Feedbacks.Interfaces;

public interface ISendFeedbackUseCase
{
    ICustomResult<bool> Handle(FeedbackDto dto, string name, string email, bool isVerified);
}