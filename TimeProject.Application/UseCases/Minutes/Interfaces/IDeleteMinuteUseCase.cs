using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.UseCases.Minutes.Interfaces;

public interface IDeleteMinuteUseCase
{
    public ICustomResult Handle(int id, int userId);
}