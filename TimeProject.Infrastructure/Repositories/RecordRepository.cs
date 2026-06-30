using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TimeProject.Domain.Entities;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;
using TimeProject.Infrastructure.Database;
using TimeProject.Infrastructure.Shared;

namespace TimeProject.Infrastructure.Repositories;

public class RecordRepository(CustomDbContext db) : IRecordRepository
{
    public IIndexRepositoryResult<Record> Index(IPaginationQuery paginationQuery, int userId)
    {
        var query = db.Records.AsQueryable();

        query = query.Where(record => record.UserId == userId);
        query = SearchWhereConditional(query, paginationQuery.Search);

        if (paginationQuery.Filters != null)
            foreach (var filter in paginationQuery.Filters)
            {
                if (string.IsNullOrEmpty(filter)) continue;
                var split = filter.Split("::");

                if (split[0] == "category")
                {
                    var id = int.Parse(split[^1]);
                    query = query.Where(record => record.CategoryId == id);
                }
            }

        var count = query.Count();

        query = paginationQuery.SortProp switch
        {
            "title" => paginationQuery.Sort == "asc"
                ? query.OrderBy(record => record.Name)
                : query.OrderByDescending(record => record.Name),

            "code" => paginationQuery.Sort == "asc"
                ? query.OrderBy(record => record.Code)
                : query.OrderByDescending(record => record.Code),

            "timeOnSeconds" => paginationQuery.Sort == "asc"
                ? query.OrderBy(record => record.Resume == null).ThenBy(record => record.Resume!.Seconds)
                : query.OrderBy(record => record.Resume == null).ThenByDescending(record => record.Resume!.Seconds),

            _ => paginationQuery.Sort == "asc" // Padrão: Último Progresso
                ? query.OrderBy(record => record.Resume == null).ThenBy(record => record.Resume!.LastDate)
                : query.OrderBy(record => record.Resume == null).ThenByDescending(record => record.Resume!.LastDate)
        };


        var entities = query
            .Skip((paginationQuery.Page - 1) * paginationQuery.PerPage)
            .Take(paginationQuery.PerPage)
            .Include(r => r.Category)
            .Include(r => r.Resume)
            .ToList();

        return new IndexRepositoryResult<Record>()
        {
            Count = count,
            Entities = entities
        };
    }

    public IList<SearchRecordItem> SearchRecord(string search, int userId)
    {
        var query = db.Records.AsQueryable();
        query = query.Where(record => record.UserId == userId);

        if (string.IsNullOrEmpty(search) == false)
            query = SearchWhereConditional(query, search);

        query = query.OrderBy(record => record.Resume == null).ThenByDescending(record => record.Resume!.LastDate);

        return query
            .Select(e => new SearchRecordItem(e.RecordId, e.Code, e.Name))
            .Take(10)
            .ToList();
    }

    public Record Create(Record entity)
    {
        db.Records.Add((Record)entity);
        return entity;
    }

    public Record Update(Record entity)
    {
        var record = (Record)entity;
        db.Records.Update(record);
        db.SaveChanges();
        return record;
    }

    public Record? Details(string code, int userId)
    {
        return db.Records
            .Include(r => r.Category)
            .Include(r => r.Resume)
            .FirstOrDefault(record => record.Code == code && record.UserId == userId);
    }

    public bool Delete(Record entity)
    {
        db.Records.Remove((Record)entity);
        db.SaveChanges();
        return true;
    }

    public Record? FindById(int id, int userId)
    {
        return db.Records
            .FirstOrDefault(record => record.RecordId == id && record.UserId == userId);
    }

    public IEnumerable<Record> FindByIdList(IEnumerable<int> idList, int userId)
    {
        return db.Records.Where(e => idList.Contains(e.RecordId))
                .Include(e => e.Category)
                .Include(e => e.Resume);
    }

    public Record? FindByCode(string code, int userId)
    {
        return db.Records
            .FirstOrDefault(record => record.Code == code && record.UserId == userId);
    }

    private static IQueryable<Record> SearchWhereConditional(IQueryable<Record> query,
        string? search)
    {
        if (string.IsNullOrWhiteSpace(search)) return query;

        return query.Where(record =>
            EF.Functions.Like(
                record.Code.ToLower(),
                $"%{search.ToLower()}%") ||
            (record.Name != null &&
             EF.Functions.Like(
                 record.Name!.ToLower(),
                 $"%{search.ToLower()}%"))
        );
    }
}