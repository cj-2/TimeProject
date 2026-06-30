namespace TimeProject.Domain.Repositories.Shared;

public class IndexResult<T>
{
    public int Count { get; set; }
    public IEnumerable<T> Entities { get; set; } = null!;
}