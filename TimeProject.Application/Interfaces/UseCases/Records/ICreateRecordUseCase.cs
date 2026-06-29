using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface ICreateRecordUseCase
{
    ICustomResult<RecordOutDto> Handle(CreateRecordDto dto, IList<PeriodDto>? periods, int userId);
}