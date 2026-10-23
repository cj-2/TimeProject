using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Minutes.Interfaces;

public interface IDeleteMinuteUseCase
{
    public ICustomResult Handle(int id, int userId);
}