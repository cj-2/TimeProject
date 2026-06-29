using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Feedbacks;

namespace TimeProject.Application.Interfaces.UseCases.Feedbacks;

public interface ISendFeedbackUseCase
{
    ICustomResult<bool> Handle(FeedbackDto dto, string name, string email, bool isVerified);
}