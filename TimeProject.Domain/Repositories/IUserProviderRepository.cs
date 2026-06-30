using TimeProject.Domain.Entities;

namespace TimeProject.Domain.Repositories;

public interface IUserProviderRepository
{
    public UserProvider Create(UserProvider entity);
    public bool Delete(int id);
    public UserProvider? FindByUserId(int userId);
    public UserProvider? FindByUserProviderId(string provider, string id);
}