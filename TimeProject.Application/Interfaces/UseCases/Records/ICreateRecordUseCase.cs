using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Periods;
using TimeProject.Infrastructure.ObjectValues.Records;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface ICreateRecordUseCase
{
    ICustomResult<RecordOutDto> Handle(CreateRecordDto dto, IList<PeriodDto>? periods, int userId);
}