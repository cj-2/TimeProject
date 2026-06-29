using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.CustomLogs;

public interface ICreateUserAccessLogUseCase
{
    ICustomResult<IUserAccessLog> Handle(IUserAccessLog entity);
}