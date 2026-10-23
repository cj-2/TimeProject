using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Shared.Interfaces;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface IGetRecordByCodeUseCase
{
    ICustomResult<RecordOutDto> Handle(string code, int userId);
}