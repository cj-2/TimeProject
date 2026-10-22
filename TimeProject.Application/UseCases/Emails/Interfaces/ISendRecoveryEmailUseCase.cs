using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.UseCases.Emails.Interfaces;

public interface ISendRecoveryEmailUseCase
{
    ICustomResult<bool> Handle(string email, string recoveryUrl);
}