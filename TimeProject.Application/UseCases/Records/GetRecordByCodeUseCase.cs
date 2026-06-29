using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Dtos.Records;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Utils.Interfaces;

namespace TimeProject.Application.UseCases.Records;

public class GetRecordByCodeUseCase(IRecordRepository repository, IRecordMapDataUtil mapDataUtil)
    : IGetRecordByCodeUseCase
{
    public ICustomResult<IRecordOutDto> Handle(string code, int userId)
    {
        var result = new CustomResult<IRecordOutDto>();
        var entity = repository.Details(code, userId);

        return entity == null
            ? result.SetError(RecordMessageErrors.NotFound)
            : result.SetData(mapDataUtil.Handle(entity));
    }
}