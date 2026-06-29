using TimeProject.Application.Dtos.Emails;

namespace TimeProject.Application.Interfaces.Handlers;

public interface IEmailHandler
{
    public void Send(EmailPayloadDto emailPayloadDto);
}