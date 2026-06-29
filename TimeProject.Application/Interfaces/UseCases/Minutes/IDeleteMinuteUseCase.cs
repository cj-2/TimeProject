using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Minutes;

public interface IDeleteMinuteUseCase
{
    public ICustomResult<bool> Handle(int id, int userId);
}