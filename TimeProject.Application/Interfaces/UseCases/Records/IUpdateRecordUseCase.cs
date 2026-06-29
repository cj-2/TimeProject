using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IUpdateRecordUseCase
{
    ICustomResult<RecordOutDto> Handle(int id, UpdateRecordDto dto, int userId);
}