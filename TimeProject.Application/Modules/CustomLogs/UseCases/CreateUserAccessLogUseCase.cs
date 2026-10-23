using TimeProject.Application.Modules.CustomLogs.CustomLogs;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.CustomLogs.UseCases;

public class CreateUserAccessLogUseCase(IUnitOfWork unitOfWork) : ICreateUserAccessLogUseCase
{
    public ICustomResult<UserAccessLog> Handle(UserAccessLog entity)
    {
        var result = unitOfWork.UserAccessLog.Create(entity);
        unitOfWork.SaveChanges();
        return new CustomResult<UserAccessLog>().SetData(result);
    }
}