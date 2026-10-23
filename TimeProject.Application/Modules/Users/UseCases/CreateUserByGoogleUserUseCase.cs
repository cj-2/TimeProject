using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Modules.Users.Dtos;
using TimeProject.Application.Modules.Users.Interfaces;
using TimeProject.Application.Shared;
using TimeProject.Domain.Entities;
using TimeProject.Domain.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.Modules.Users.UseCases;

public class CreateUserByGoogleUserUseCase(IUnitOfWork unitOfWork) : ICreateUserByGoogleUserUseCase
{
    public ICustomResult<User> Handle(CreateUserOAuthDto dto, string email)
    {
        var result = new CustomResult<User>();

        if (string.IsNullOrEmpty(dto.UserProviderId))
            return result.SetError(UserErrors.OAuthWithoutProviderId);

        var userWithEmail = unitOfWork.User.FindByEmail(email);

        if (userWithEmail != null)
        {
            unitOfWork.UserProvider.Create(new UserProvider
            {
                UserId = userWithEmail.UserId,
                Provider = "google",
                ExternalId = dto.UserProviderId
            });

            unitOfWork.SaveChanges();

            return result.SetData(userWithEmail);
        }

        var userEntity = unitOfWork.User
            .Create(new User
            {
                Name = dto.Name,
                Email = email,
                Timezone = ""
            });

        unitOfWork.SaveChanges();

        unitOfWork.UserProvider.Create(new UserProvider
        {
            UserId = userEntity.UserId,
            Provider = "google",
            ExternalId = dto.UserProviderId
        });

        unitOfWork.SaveChanges();

        return result.SetData(userEntity);
    }
}