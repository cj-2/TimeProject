using AutoMapper;
using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.Utils;
using TimeProject.Domain.Entities;

namespace TimeProject.Application.Utils;

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