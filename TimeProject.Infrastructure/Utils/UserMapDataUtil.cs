using AutoMapper;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Infrastructure.Utils;

public class UserMapDataUtil(IMapper mapper) : IUserMapDataUtil
{
    public UserOutDto Handle(IUser entity)
    {
        return mapper.Map<IUser, UserOutDto>(entity);
    }

    public IEnumerable<UserOutDto> Handle(IEnumerable<IUser> entity)
    {
        return mapper.Map<IEnumerable<IUser>, IEnumerable<UserOutDto>>(entity);
    }
}