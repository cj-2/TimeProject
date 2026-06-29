using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Shared;
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