using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Records;
using TimeProject.Application.Shared;
using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Repositories;

namespace TimeProject.Application.UseCases.Records;

public class SearchRecordUseCase(IRecordRepository repository) : ISearchRecordUseCase
{
    public ICustomResult<IList<SearchRecordItem>> Handle(string search, int userId)
    {
        var result = new CustomResult<IList<SearchRecordItem>>();
        return result.SetData(repository.SearchRecord(search, userId));
    }
}