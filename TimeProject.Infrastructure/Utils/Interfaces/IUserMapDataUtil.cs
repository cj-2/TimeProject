using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Infrastructure.Utils.Interfaces;

public interface IUserMapDataUtil
{
    UserOutDto Handle(IUser entity);
    IEnumerable<UserOutDto> Handle(IEnumerable<IUser> entity);
}