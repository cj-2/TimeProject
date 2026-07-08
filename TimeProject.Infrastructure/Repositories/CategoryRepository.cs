using Dapper;
using Microsoft.EntityFrameworkCore;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Repositories.Shared;
using TimeProject.Infrastructure.Database;

namespace TimeProject.Infrastructure.Repositories;

public class CategoryRepository(CustomDbContext context) : ICategoryRepository
{
    public List<Category> Index(int userId, bool onlyWithData)
    {
        var sql = onlyWithData
            ? """
              select category_id as CategoryId, name as Name, user_id as UserId
              from categories c
              where 
                  exists(select 1 from records r where r.category_id = c.category_id)
                  and user_id = @Id;
              """
            : """
              select category_id as CategoryId, name as Name, user_id as UserId
              from categories
              where user_id = @Id;
              """;

        return context.Database.GetDbConnection()
            .Query<Category>(sql, new { Id = userId })
            .ToList();
    }

    public IList<Category> Index(PaginationQuery paginationQuery, int userId)
    {
        IQueryable<Category> query = context.Categories;
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
        IQueryable<Category> query = context.Categories;
        query = query.Where(c => c.UserId == userId);

        if (!string.IsNullOrWhiteSpace(paginationQuery.Search))
            query = SearchWhereConditional(query, paginationQuery.Search);

        return query.Count();
    }

    public Category Create(Category entity)
    {
        context.Categories.Add(entity);
        context.SaveChanges();
        return entity;
    }

    public Category Update(Category entity)
    {
        context.Categories.Update(entity);
        context.SaveChanges();
        return entity;
    }

    public bool Delete(Category entity)
    {
        context.Categories.Remove(entity);
        context.SaveChanges();
        return true;
    }

    public Category? FindById(int id)
    {
        return context.Categories.FirstOrDefault(c => c.CategoryId == id);
    }

    public Category? FindById(int id, int userId)
    {
        return context.Categories.FirstOrDefault(c => c.CategoryId == id && c.UserId == userId);
    }

    public Category? FindByName(string name, int userId)
    {
        return context.Categories.FirstOrDefault(category => category.Name == name && category.UserId == userId);
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