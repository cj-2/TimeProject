using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.UseCases.Emails.Interfaces;

public interface ISendRegisterEmailUseCase
{
    public ICustomResult<bool> Handle(string email, string verifyUrl);
}