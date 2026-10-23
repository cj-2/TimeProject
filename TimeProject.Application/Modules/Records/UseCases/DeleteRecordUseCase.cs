using TimeProject.Application.Modules.Records.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Errors;

namespace TimeProject.Application.Modules.Records.UseCases;

public class DeleteRecordUseCase(IRecordRepository repository) : IDeleteRecordUseCase
{
    public ICustomResult<bool> Handle(int id, int userId)
    {
        var result = new CustomResult<bool>();
        var entity = repository.FindById(id, userId);

        return entity == null
            ? result.SetError(RecordErrors.NotFound)
            : result.SetData(repository.Delete(entity));
    }
}