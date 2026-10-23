using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Users.Interfaces;

public interface IGetUserByOAtuhProviderIdUseCase
{
    ICustomResult<User> Handle(string provider, string id);
}