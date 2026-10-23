using TimeProject.Application.Modules.Records.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.Modules.Records.UseCases;

public class GetRecordByIdUseCase(IRecordRepository repository) : IGetRecordByIdUseCase
{
    public ICustomResult<Record> Handle(int id, int userId)
    {
        var result = new CustomResult<Record>();
        var entity = repository.FindById(id, userId);

        return entity == null
            ? result.SetError(RecordErrors.NotFound)
            : result.SetData(entity);
    }
}