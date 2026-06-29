using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetRecordByCodeUseCase
{
    ICustomResult<RecordOutDto> Handle(string code, int userId);
}