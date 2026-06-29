using TimeProject.Domain.Dtos.Periods;
using TimeProject.Domain.Dtos.Records;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface ICreateRecordUseCase
{
    ICustomResult<IRecordOutDto> Handle(ICreateRecordData data, IList<IPeriodData>? periods, int userId);
}