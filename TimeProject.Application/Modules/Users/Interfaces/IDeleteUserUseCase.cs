using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface IDeleteUserUseCase
{
    ICustomResult<bool> Handle(int id);
}