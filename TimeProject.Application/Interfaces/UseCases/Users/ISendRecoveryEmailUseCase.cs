using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface ISendRecoveryEmailUseCase
{
    ICustomResult<bool> Handle(string email, string recoveryUrl);
}