using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface IDeleteUserUseCase
{
    ICustomResult<bool> Handle(int id);
}