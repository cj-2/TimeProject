using TimeProject.Application.Dtos.Periods;
using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface ICreateRecordUseCase
{
    ICustomResult<RecordOutDto> Handle(CreateRecordDto dto, IList<PeriodDto>? periods, int userId);
}