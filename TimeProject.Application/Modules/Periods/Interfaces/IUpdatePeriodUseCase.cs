using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Periods.Interfaces;

public interface IUpdatePeriodUseCase
{
    ICustomResult<Period> Handle(int id, PeriodDto dto, int userId);
}