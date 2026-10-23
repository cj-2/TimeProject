using TimeProject.Application.Modules.Periods.Dtos;
using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface ICreateRecordUseCase
{
    ICustomResult<RecordOutDto> Handle(CreateRecordDto dto, IList<PeriodDto>? periods, int userId);
}