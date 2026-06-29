using TimeProject.Application.Interfaces.UseCases.Feedbacks;
using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Factories;
using TimeProject.Infrastructure.ObjectValues.Feedbacks;

namespace TimeProject.Application.UseCases.Feedbacks;

public class SendFeedbackUseCase(IHookHandler hookHandler) : ISendFeedbackUseCase
{
    public ICustomResult<bool> Handle(FeedbackDto dto, string name, string email, bool isVerified)
    {
        hookHandler.Send(HookTo.Feedbacks,
            FeedbackFactory.Create(dto.Message, false, name, email, isVerified));

        return new CustomResult<bool> { Data = true };
    }
}