using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface IUpdateRecordUseCase
{
    ICustomResult<RecordOutDto> Handle(int id, UpdateRecordDto dto, int userId);
}