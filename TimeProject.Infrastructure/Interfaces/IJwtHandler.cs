using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Domain.Dtos.Auths;
using TimeProject.Infrastructure.ObjectValues.Auths;
using JwtResult = TimeProject.Infrastructure.ObjectValues.Auths.JwtResult;

namespace TimeProject.Infrastructure.Interfaces;

public interface IJwtHandler
{
    JwtResult Generate(IUser user);
}