using TimeProject.Domain.ObjectValues;

namespace TimeProject.Application.Dtos.General;

public class Pagination<T> : IPagination<T>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public IEnumerable<T>? Data { get; set; }
    public string? Search { get; set; }
    public string? OrderBy { get; set; }
    public string? Sort { get; set; }
    public string? SortProp { get; set; }

    public static IPagination<T> Handle(
        IEnumerable<T> data,
        int page,
        int perPage,
        int totalItems,
        string search = "",
        string orderBy = "",
        string sort = "",
        string sortProp = "")
    {
        return new Pagination<T>
        {
            Page = page,
            PerPage = perPage,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling((float)totalItems / perPage),
            Search = search,
            OrderBy = orderBy,
            Sort = sort,
            SortProp = sortProp,
            Data = data
        };
    }

    public static Pagination<T> Handle(
        IEnumerable<T> data,
        IPaginationQuery paginationQuery,
        int totalItems)
    {
        return new Pagination<T>
        {
            Page = paginationQuery.Page,
            PerPage = paginationQuery.PerPage,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling((float)totalItems / paginationQuery.PerPage),
            Search = paginationQuery.Search,
            OrderBy = paginationQuery.OrderBy,
            Sort = paginationQuery.Sort,
            SortProp = paginationQuery.SortProp,
            Data = data
        };
    }
}