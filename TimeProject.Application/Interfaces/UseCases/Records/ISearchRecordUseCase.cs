using TimeProject.Domain.ObjectValues;
using TimeProject.Domain.Shared;

namespace TimeProject.Application.Interfaces.UseCases.Records;

public interface ISearchRecordUseCase
{
    ICustomResult<IList<SearchRecordItem>> Handle(string search, int userId);
}