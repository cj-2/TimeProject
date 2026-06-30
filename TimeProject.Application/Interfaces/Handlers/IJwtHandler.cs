using TimeProject.Application.Dtos.Auths;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.Handlers;

public interface IJwtHandler
{
    JwtDto Generate(User user);
}