using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Periods.Interfaces;

public interface IDeletePeriodUseCase
{
    ICustomResult<bool> Handle(int id, int userId);
}