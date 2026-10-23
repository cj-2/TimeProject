using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Periods;
using TimeProject.Application.Shared;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Periods;

public class DeletePeriodUseCase(IUnitOfWork unit)
    : IDeletePeriodUseCase
{
    public ICustomResult<bool> Handle(int id, int userId)
    {
        var result = new CustomResult<bool>();
        var period = unit.PeriodRepository.FindById(id, userId);

        if (period == null)
            return result.SetError(PeriodErrors.NotFound);

        var data = unit.PeriodRepository.Delete(period);
        return result.SetData(data);
    }
}