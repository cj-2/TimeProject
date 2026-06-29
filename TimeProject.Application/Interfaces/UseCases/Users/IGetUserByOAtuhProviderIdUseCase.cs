using TimeProject.Domain.Entities;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IGetUserByOAtuhProviderIdUseCase
{
    ICustomResult<IUser> Handle(string provider, string id);
}