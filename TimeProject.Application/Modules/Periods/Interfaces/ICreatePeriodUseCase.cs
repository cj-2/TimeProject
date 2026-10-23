using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Periods.Interfaces;

public interface ICreatePeriodUseCase
{
    ICustomResult<Period> Handle(CreatePeriodDto data, int userId);
}