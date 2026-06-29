using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Records;

public class GetRecordByIdUseCase(IRecordRepository repository) : IGetRecordByIdUseCase
{
    public ICustomResult<IRecord> Handle(int id, int userId)
    {
        var result = new CustomResult<IRecord>();
        var entity = repository.FindById(id, userId);

        return entity == null
            ? result.SetError(RecordMessageErrors.NotFound)
            : result.SetData(entity);
    }
}