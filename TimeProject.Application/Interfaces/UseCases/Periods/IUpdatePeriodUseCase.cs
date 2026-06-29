using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Periods;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface IUpdatePeriodUseCase
{
    ICustomResult<IPeriod> Handle(int id, PeriodDto dto, int userId);
}