using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.CustomLogs;

public interface ICreateUserAccessLogUseCase
{
    ICustomResult<UserAccessLog> Handle(UserAccessLog entity);
}