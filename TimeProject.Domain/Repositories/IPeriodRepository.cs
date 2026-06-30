using TimeProject.Domain.Entities;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Domain.Repositories;

public interface IPeriodRepository
{
    IList<Period> Index(int recordId, int userId, IPaginationQuery paginationQuery);
    int GetTotalItems(int recordId, IPaginationQuery paginationQuery, int userId);
    Period Create(Period entity);
    IList<Period> CreateByList(IList<Period> entities);
    bool DeleteByList(IList<Period> entityList);
    Period Update(Period entity);
    bool Delete(Period entity);
    Period? FindById(int id, int userId);
}