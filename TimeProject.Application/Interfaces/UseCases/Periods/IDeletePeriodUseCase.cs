using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface IDeletePeriodUseCase
{
    ICustomResult<bool> Handle(int id, int userId);
}