using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Infrastructure.Database.Entities;
using TimeProject.Infrastructure.Utils.Interfaces;
using TimeProject.Domain.Shared;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;
using TimeProject.Infrastructure.ObjectValues;
using TimeProject.Infrastructure.ObjectValues.Users;

namespace TimeProject.Application.UseCases.Users;

public class CreateUserUseCase(
    IUnitOfWork unitOfWork,
    IUserMapDataUtil mapper,
    IJwtHandler jwtHandler,
    ICreateOrUpdateUserPasswordUseCase createUserPasswordUseCase
) : ICreateUserUseCase
{
    public ICustomResult<CreateUserOutDto> Handle(CreateUserDto dto)
    {
        var result = new CustomResult<CreateUserOutDto>();
        var emailAvailable = unitOfWork.UserRepository.EmailIsAvailable(dto.Email);

        if (emailAvailable == false) return result.SetError(UserMessageErrors.EmailAlreadyInUse);

        var entity = unitOfWork.UserRepository.Create(new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Timezone = dto.Timezone
        });

        unitOfWork.SaveChanges();

        createUserPasswordUseCase.Handle(entity.UserId,
            new CreatePasswordDto { Password = dto.Password },
            saveChanges: false);

        unitOfWork.SaveChanges();
        
        result.Data = new CreateUserOutDto
        {
            User = mapper.Handle(entity),
            Jwt = jwtHandler.Generate(entity)
        };

        // hookHandler.Send(HookTo.Users,
        //     $"<b>{dto.Name}</b> acabou de criar uma conta com o email:\n<b>{dto.Email}</b>");

        return result;
    }
}