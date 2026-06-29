using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IDeleteUserUseCase
{
    ICustomResult<bool> Handle(int id);
}