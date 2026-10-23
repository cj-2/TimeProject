using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface ICreateOrUpdateUserPasswordByEmailUseCase
{
    ICustomResult<bool> Handle(string email, string password);
}