using TimeProject.Application.Modules.Auth.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.Handlers;

public interface IJwtHandler
{
    JwtDto Generate(User user);
}