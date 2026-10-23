using TimeProject.Application.Modules.Periods.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Periods.UseCases;

public class DeletePeriodUseCase(IUnitOfWork unit)
    : IDeletePeriodUseCase
{
    public ICustomResult<bool> Handle(int id, int userId)
    {
        var result = new CustomResult<bool>();
        var period = unit.Period.FindById(id, userId);

        if (period == null)
            return result.SetError(PeriodErrors.NotFound);

        var data = unit.Period.Delete(period);
        return result.SetData(data);
    }
}