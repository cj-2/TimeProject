using TimeProject.Domain.Dtos.Records;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetRecordByCodeUseCase
{
    ICustomResult<IRecordOutDto> Handle(string code, int userId);
}