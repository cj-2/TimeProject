using TimeProject.Application.Interfaces.UseCases.Feedbacks;
using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Factories;
using TimeProject.Infrastructure.ObjectValues.Feedbacks;

namespace TimeProject.Application.UseCases.Feedbacks;

public class SendPublicFeedbackUseCase(IHookHandler hookHandler) : ISendPublicFeedbackUseCase
{
    public ICustomResult<bool> Handle(PublicFeedbackDto dto)
    {
        hookHandler.Send(HookTo.Feedbacks,
            FeedbackFactory.Create(dto.Message, true, dto.Name, dto.Email));

        return new CustomResult<bool> { Data = true };
    }
}