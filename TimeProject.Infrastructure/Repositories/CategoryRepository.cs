using Microsoft.EntityFrameworkCore;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.Shared;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Repositories;

public class CategoryRepository(CustomDbContext db) : ICategoryRepository
{
    public IList<Category> Index(int userId, bool onlyWithData)
    {
        return onlyWithData
            ? db.Records
                .Where(e => e.Category != null && e.UserId == userId)
                .Select(e => e.Category)
                .Distinct()!
                .ToList<Category>()
            : db.Categories
                .Where(category => category.UserId == userId)
                .ToList<Category>();
    }

    public IList<Category> Index(PaginationQuery paginationQuery, int userId)
    {
        IQueryable<Category> query = db.Categories;
        query = query.Where(c => c.UserId == userId);

        if (!string.IsNullOrWhiteSpace(paginationQuery.Search))
            query = SearchWhereConditional(query, paginationQuery.Search);

        if (string.IsNullOrWhiteSpace(paginationQuery.Sort) || paginationQuery.Sort == "asc")
            query = query.OrderBy(c => c.Name);
        else
            query = query.OrderByDescending(c => c.Name);

        return query
            .Skip((paginationQuery.Page - 1) * paginationQuery.PerPage)
            .Take(paginationQuery.PerPage)
            .ToList();
    }

    public int GetTotalItems(PaginationQuery paginationQuery, int userId)
    {
        IQueryable<Category> query = db.Categories;
        query = query.Where(c => c.UserId == userId);

        if (!string.IsNullOrWhiteSpace(paginationQuery.Search))
            query = SearchWhereConditional(query, paginationQuery.Search);

        return query.Count();
    }

    public Category Create(Category entity)
    {
        db.Categories.Add((Category)entity);
        db.SaveChanges();
        return entity;
    }

    public Category Update(Category entity)
    {
        db.Categories.Update((Category)entity);
        db.SaveChanges();
        return entity;
    }

    public bool Delete(Category entity)
    {
        db.Categories.Remove((Category)entity);
        db.SaveChanges();
        return true;
    }

    public Category? FindById(int id)
    {
        return db.Categories.FirstOrDefault(c => c.CategoryId == id);
    }

    public Category? FindById(int id, int userId)
    {
        return db.Categories.FirstOrDefault(c => c.CategoryId == id && c.UserId == userId);
    }

    public Category? FindByName(string name, int userId)
    {
        return db.Categories.FirstOrDefault(category => category.Name == name && category.UserId == userId);
    }

    private static IQueryable<Category> SearchWhereConditional(IQueryable<Category> query, string search)
    {
        return query.Where(c =>
            EF.Functions.Like(
                c.Name.ToLower(),
                $"%{search.ToLower()}%")
        );
    }
}