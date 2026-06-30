using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface IUpdatePeriodUseCase
{
    ICustomResult<Period> Handle(int id, PeriodDto dto, int userId);
}