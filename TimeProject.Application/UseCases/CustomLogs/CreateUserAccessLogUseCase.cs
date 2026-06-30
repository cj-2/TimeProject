using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.CustomLogs;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.CustomLogs;

public class CreateUserAccessLogUseCase(IUnitOfWork unitOfWork) : ICreateUserAccessLogUseCase
{
    public ICustomResult<UserAccessLog> Handle(UserAccessLog entity)
    {
        var result = unitOfWork.UserAccessLogRepository.Create(entity);
        unitOfWork.SaveChanges();
        return new CustomResult<UserAccessLog>().SetData(result);
    }
}