using TimeProject.Application.Dtos.Records;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Records;

public class GetRecordByCodeUseCase(IRecordRepository repository, IRecordMapDataUtil mapDataUtil)
    : IGetRecordByCodeUseCase
{
    public ICustomResult<RecordOutDto> Handle(string code, int userId)
    {
        var result = new CustomResult<RecordOutDto>();
        var entity = repository.Details(code, userId);

        return entity == null
            ? result.SetError(RecordMessageErrors.NotFound)
            : result.SetData(mapDataUtil.Handle(entity));
    }
}