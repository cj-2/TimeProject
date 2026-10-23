using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface ICreateOrUpdateUserPasswordByEmailUseCase
{
    ICustomResult<bool> Handle(string email, string password);
}