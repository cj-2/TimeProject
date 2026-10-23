using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Sessions.Interfaces;

public interface IDeleteSessionUseCase
{
    ICustomResult<bool> Handle(int id, int userId);
}