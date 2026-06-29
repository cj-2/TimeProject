using TimeProject.Domain.Dtos.Periods;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface ICreatePeriodUseCase
{
    ICustomResult<IPeriod> Handle(ICreatePeriodData data, int userId);
}