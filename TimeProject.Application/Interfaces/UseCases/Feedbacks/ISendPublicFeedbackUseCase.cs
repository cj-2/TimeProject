using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Feedbacks;

namespace TimeProject.Application.Interfaces.UseCases.Feedbacks;

public interface ISendPublicFeedbackUseCase
{
    ICustomResult<bool> Handle(PublicFeedbackDto dto);
}