using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface IGetRecordByIdUseCase
{
    ICustomResult<Record> Handle(int id, int userId);
}