using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface IUpdateRecordUseCase
{
    ICustomResult<RecordOutDto> Handle(int id, UpdateRecordDto dto, int userId);
}