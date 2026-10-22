using TimeProject.Application.Interfaces.Shared;
using TimeProject.Application.Shared;
using TimeProject.Application.UseCases.Users.Stories.Dtos;
using TimeProject.Application.UseCases.Users.Stories.Interfaces;
using TimeProject.Domain.Entities;
using TimeProject.Infrastructure.Errors;
using TimeProject.Infrastructure.Interfaces;

namespace TimeProject.Application.UseCases.Users.Stories;

public class CreateUserByGoogleUserUseCase(IUnitOfWork unitOfWork) : ICreateUserByGoogleUserUseCase
{
    public ICustomResult<User> Handle(CreateUserOAuthDto dto, string email)
    {
        var result = new CustomResult<User>();

        if (string.IsNullOrEmpty(dto.UserProviderId))
            return result.SetError(UserMessageErrors.OAuthWithoutProviderId);

        var userWithEmail = unitOfWork.UserRepository.FindByEmail(email);

        if (userWithEmail != null)
        {
            unitOfWork.UserProviderRepository.Create(new UserProvider
            {
                UserId = userWithEmail.UserId,
                Provider = "google",
                ExternalId = dto.UserProviderId
            });

            unitOfWork.SaveChanges();

            return result.SetData(userWithEmail);
        }

        var userEntity = unitOfWork.UserRepository
            .Create(new User
            {
                Name = dto.Name,
                Email = email,
                Timezone = ""
            });

        unitOfWork.SaveChanges();

        unitOfWork.UserProviderRepository.Create(new UserProvider
        {
            UserId = userEntity.UserId,
            Provider = "google",
            ExternalId = dto.UserProviderId
        });

        unitOfWork.SaveChanges();

        return result.SetData(userEntity);
    }
}