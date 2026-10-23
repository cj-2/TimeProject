using AutoMapper;
using TimeProject.Application.Modules.Codes.Interfaces;
using TimeProject.Application.Modules.Codes.Dtos;
using TimeProject.Application.Shared;
using TimeProject.Application.Shared.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Repositories;
using TimeProject.Domain.Entities.Enums;

namespace TimeProject.Application.Modules.Codes.UseCases;

public class GetRegisterCodeInfoUseCase(IConfirmCodeRepository repository, IMapper mapper) : IGetRegisterCodeInfoUseCase
{
    public ICustomResult<ConfirmCodeOutDto> Handle(int userId)
    {
        var result = new CustomResult<ConfirmCodeOutDto>();
        var codes = repository.FindByUserIdThatIsNotExpiredOrUsed(userId, ConfirmCodeType.Register);

        return codes.Count != 0
            ? result.SetData(mapper.Map<ConfirmCode, ConfirmCodeOutDto>(codes.First()))
            : result.SetError("not_found:code_not_found");
    }
}