using TimeProject.Application.Interfaces.UseCases.Feedbacks;
using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Dtos.Feedbacks;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Factories;

namespace TimeProject.Application.UseCases.Feedbacks;

public class SendPublicFeedbackUseCase(IHookHandler hookHandler) : ISendPublicFeedbackUseCase
{
    public ICustomResult<bool> Handle(IPublicFeedbackDto feedbackDto)
    {
        hookHandler.Send(HookTo.Feedbacks,
            FeedbackFactory.Create(feedbackDto.Message, true, feedbackDto.Name, feedbackDto.Email));

        return new CustomResult<bool> { Data = true };
    }
}