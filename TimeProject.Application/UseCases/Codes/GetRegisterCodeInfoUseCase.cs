using AutoMapper;
using TimeProject.Application.Dtos.Codes;
using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Interfaces.UseCases.Codes;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Application.UseCases.Codes;

public class GetRegisterCodeInfoUseCase(IConfirmCodeRepository repository, IMapper mapper) : IGetRegisterCodeInfoUseCase
{
    public ICustomResult<ConfirmCodeOutDto> Handle(int userId)
    {
        var result = new CustomResult<ConfirmCodeOutDto>();
        var codes = repository.FindByUserIdThatIsNotExpiredOrUsed(userId, ConfirmCodeType.Register);

        return codes.Count != 0
            ? result.SetData(mapper.Map<IConfirmCode, ConfirmCodeOutDto>(codes.First()))
            : result.SetError("not_found:code_not_found");
    }
}