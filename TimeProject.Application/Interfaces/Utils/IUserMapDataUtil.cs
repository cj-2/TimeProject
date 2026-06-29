using TimeProject.Application.Dtos.Users;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Interfaces.Utils;

public interface IUserMapDataUtil
{
    UserOutDto Handle(IUser entity);
    IEnumerable<UserOutDto> Handle(IEnumerable<IUser> entity);
}