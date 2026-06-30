using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface IGetRecordByIdUseCase
{
    ICustomResult<Record> Handle(int id, int userId);
}