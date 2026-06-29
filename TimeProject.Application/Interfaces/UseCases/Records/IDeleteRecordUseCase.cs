using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IDeleteRecordUseCase
{
    ICustomResult<bool> Handle(int id, int userId);
}