using TimeProject.Domain.Entities;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Repositories;

public class PeriodRepository(CustomDbContext db) : IPeriodRepository
{
    public IList<Period> Index(int recordId, int userId, IPaginationQuery paginationQuery)
    {
        return db.Periods
            .Where(period => period.RecordId == recordId && period.UserId == userId)
            .OrderByDescending(period => period.Start)
            .Skip((paginationQuery.Page - 1) * paginationQuery.PerPage)
            .Take(paginationQuery.PerPage)
            .ToList<Period>();
    }

    public int GetTotalItems(int recordId, IPaginationQuery paginationQuery, int userId)
    {
        return db.Periods
            .Count(period => period.RecordId == recordId && period.UserId == userId);
    }

    public Period Create(Period entity)
    {
        db.Periods.Add(entity);
        db.SaveChanges();
        return entity;
    }

    public IList<Period> CreateByList(IList<Period> entities)
    {
        db.Periods.AddRange(entities);
        db.SaveChanges();
        return entities;
    }

    public Period Update(Period entity)
    {
        db.Periods.Update(entity);
        db.SaveChanges();
        return entity;
    }

    public bool Delete(Period entity)
    {
        db.Periods.Remove(entity);
        db.SaveChanges();
        return true;
    }

    public bool DeleteByList(IList<Period> entityList)
    {
        db.Periods.RemoveRange((entityList as IList<Period>)!);
        db.SaveChanges();
        return true;
    }

    public Period? FindById(int id, int userId)
    {
        return db.Periods
            .FirstOrDefault(period => period.PeriodId == id && period.UserId == userId);
    }
}