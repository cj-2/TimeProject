using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Repositories;

public interface IUserAccessLogRepository
{
    public UserAccessLog Create(UserAccessLog entity);
    public UserAccessLog? GetLastAccessByUserId(int id);
    public IList<UserAccessLog> GetLastAccessByUserIdList(IEnumerable<int> idList);
}