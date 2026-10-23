using TimeProject.Application.Modules.Email.Dtos;

namespace TimeProject.Application.Modules.Email.Interfaces;

public interface IEmailHandler
{
    public void Send(EmailPayloadDto emailPayloadDto);
}