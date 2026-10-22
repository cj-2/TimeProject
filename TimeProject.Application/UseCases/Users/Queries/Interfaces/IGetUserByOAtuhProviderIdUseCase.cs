using TimeProject.Application.Interfaces.Shared;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.UseCases.Users.Queries.Interfaces;

public interface IGetUserByOAtuhProviderIdUseCase
{
    ICustomResult<User> Handle(string provider, string id);
}