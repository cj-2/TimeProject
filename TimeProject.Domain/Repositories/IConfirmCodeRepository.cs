using TimeProject.Domain.Entities;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Domain.Repositories;

public interface IConfirmCodeRepository
{
    ConfirmCode Create(ConfirmCode entity);
    ConfirmCode Update(ConfirmCode entity);
    ConfirmCode? FindById(string id);
    ConfirmCode? FindByIdAndEmail(string id, string email);
    IList<ConfirmCode> FindByUserId(int userId);
    IList<ConfirmCode> FindByUserId(int userId, ConfirmCodeType type);
    IList<ConfirmCode> FindByUserIdThatIsNotExpiredOrUsed(int userId, ConfirmCodeType type);
}