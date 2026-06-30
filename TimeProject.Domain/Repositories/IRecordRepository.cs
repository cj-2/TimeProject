using TimeProject.Domain.Entities;
using TimeProject.Domain.ObjectValues;

namespace TimeProject.Domain.Repositories;

public interface IRecordRepository
{
    IIndexRepositoryResult<Record> Index(IPaginationQuery paginationQuery, int userId);
    IList<SearchRecordItem> SearchRecord(string search, int userId);
    Record Create(Record entity);
    Record Update(Record entity);
    bool Delete(Record entity);
    Record? FindById(int id, int userId);
    IEnumerable<Record> FindByIdList(IEnumerable<int> idList, int userId);
    Record? FindByCode(string code, int userId);
    Record? Details(string code, int userId);
}