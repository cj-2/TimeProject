using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.ObjectValues.Records;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetRecordByCodeUseCase
{
    ICustomResult<RecordOutDto> Handle(string code, int userId);
}