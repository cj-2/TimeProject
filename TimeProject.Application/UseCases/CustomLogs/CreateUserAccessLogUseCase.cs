using TimeProject.Application.Interfaces.UseCases.CustomLogs;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.CustomLogs;

public class CreateUserAccessLogUseCase(IUnitOfWork unitOfWork) : ICreateUserAccessLogUseCase
{
    public ICustomResult<IUserAccessLog> Handle(IUserAccessLog entity)
    {
        var result = unitOfWork.UserAccessLogRepository.Create(entity);
        unitOfWork.SaveChanges();
        return new CustomResult<IUserAccessLog>().SetData(result);
    }
}