using TimeProject.Domain.Dtos.Periods;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface IUpdatePeriodUseCase
{
    ICustomResult<IPeriod> Handle(int id, IPeriodData data, int userId);
}