using TimeProject.Application.Modules.Records.Dtos;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Modules.Records.Interfaces;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.Modules.Records.UseCases;

public class GetRecordByCodeUseCase(IRecordRepository repository, IRecordMapDataUtil mapDataUtil)
    : IGetRecordByCodeUseCase
{
    public ICustomResult<RecordOutDto> Handle(string code, int userId)
    {
        var result = new CustomResult<RecordOutDto>();
        var entity = repository.Details(code, userId);

        return entity == null
            ? result.SetError(RecordErrors.NotFound)
            : result.SetData(mapDataUtil.Handle(entity));
    }
}