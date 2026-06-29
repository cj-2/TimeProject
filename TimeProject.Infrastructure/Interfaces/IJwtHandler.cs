using TimeProject.Domain.Entities;
using JwtResult = TimeProject.Infrastructure.ObjectValues.Auths.JwtResult;

namespace TimeProject.Infrastructure.Interfaces;

public interface IJwtHandler
{
    JwtResult Generate(IUser user);
}