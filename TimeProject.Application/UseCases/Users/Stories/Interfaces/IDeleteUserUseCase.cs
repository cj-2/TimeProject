using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.UseCases.Users.Stories.Interfaces;

public interface IDeleteUserUseCase
{
    ICustomResult<bool> Handle(int id);
}