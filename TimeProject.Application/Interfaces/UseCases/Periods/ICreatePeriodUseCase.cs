using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Records;

namespace TimeProject.Application.Interfaces.UseCases.Periods;

public interface ICreatePeriodUseCase
{
    ICustomResult<IPeriod> Handle(CreatePeriodDto data, int userId);
}