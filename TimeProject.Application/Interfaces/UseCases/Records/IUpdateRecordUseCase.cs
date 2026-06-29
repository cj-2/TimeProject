using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Records;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IUpdateRecordUseCase
{
    ICustomResult<RecordOutDto> Handle(int id, UpdateRecordDto dto, int userId);
}