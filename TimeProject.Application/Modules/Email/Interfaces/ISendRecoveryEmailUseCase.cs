using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Email.Interfaces;

public interface ISendRecoveryEmailUseCase
{
    ICustomResult<bool> Handle(string email, string recoveryUrl);
}