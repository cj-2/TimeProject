using TimeProject.Application.Modules.Auth.Dtos;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Modules.Auth.Interfaces;

public interface IJwtHandler
{
    JwtDto Generate(User user);
}