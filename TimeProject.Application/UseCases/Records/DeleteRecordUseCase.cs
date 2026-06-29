using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Shared;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Errors;

namespace TimeProject.Application.UseCases.Records;

public class DeleteRecordUseCase(IRecordRepository repository) : IDeleteRecordUseCase
{
    public ICustomResult<bool> Handle(int id, int userId)
    {
        var result = new CustomResult<bool>();
        var entity = repository.FindById(id, userId);

        return entity == null
            ? result.SetError(RecordMessageErrors.NotFound)
            : result.SetData(repository.Delete(entity));
    }
}