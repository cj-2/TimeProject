using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Sessions;

public interface IDeleteSessionUseCase
{
    ICustomResult<bool> Handle(int id, int userId);
}