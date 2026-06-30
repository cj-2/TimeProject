using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.UseCases.Users;

public interface IGetUserByOAtuhProviderIdUseCase
{
    ICustomResult<User> Handle(string provider, string id);
}