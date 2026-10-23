using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface ICreatePeriodUseCase
{
    ICustomResult<Period> Handle(CreatePeriodDto data, int userId);
}