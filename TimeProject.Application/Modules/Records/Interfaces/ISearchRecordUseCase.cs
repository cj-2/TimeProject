using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Repositories.ObjectValues;
using TimeProject.Domain.Repositories.Shared;

namespace TimeProject.Application.Modules.Records.Interfaces;

public interface ISearchRecordUseCase
{
    ICustomResult<IList<SearchRecordItem>> Handle(string search, int userId);
}