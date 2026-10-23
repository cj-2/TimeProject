using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.CustomLogs.CustomLogs;

public interface ICreateUserAccessLogUseCase
{
    ICustomResult<UserAccessLog> Handle(UserAccessLog entity);
}