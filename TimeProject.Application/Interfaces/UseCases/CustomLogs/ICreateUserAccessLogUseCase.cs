using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.CustomLogs;

public interface ICreateUserAccessLogUseCase
{
    ICustomResult<IUserAccessLog> Handle(IUserAccessLog entity);
}