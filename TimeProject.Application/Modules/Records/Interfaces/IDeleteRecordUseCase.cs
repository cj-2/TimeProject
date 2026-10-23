using TimeProject.Application.Interfaces.Shared;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface IDeleteRecordUseCase
{
    ICustomResult<bool> Handle(int id, int userId);
}