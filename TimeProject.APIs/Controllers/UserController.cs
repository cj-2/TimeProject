using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeProject.APIs.Controllers.Shared;
using TimeProject.Domain.ObjectValues;
using TimeProject.APIs.Controllers.Attributes;
using TimeProject.Application.Dtos.General;
using TimeProject.Application.Dtos.Users;
using TimeProject.Application.Interfaces.UseCases.Users;
using TimeProject.Application.Utils;

namespace TimeProject.APIs.Controllers;

[ApiController]
[Route("users")]
public class UserController(
    ICreateUserUseCase createUserUseCase,
    IUpdateUserUseCase updateUserUseCase,
    IUpdateUserRoleUseCase updateUserRoleUseCase,
    IDisableUserUseCase disableUserUseCase,
    IGetUserUseCase getUserUseCase,
    IDeleteUserUseCase deleteUserUseCase,
    IGetPaginatedUserUseCase getPaginatedUserUseCase
) : CustomController
{
    [HttpGet]
    [Authorize(Policy = "IsAdmin")]
    public ActionResult<IPagination<UserOutDto>> Index([FromQuery] PaginationQuery paginationQuery)
    {
        return HandleResponse(getPaginatedUserUseCase.Handle(paginationQuery));
    }

    [HttpPost]
    [UserChallenge]
    public ActionResult<CreateUserOutDto> Create([FromBody] CreateUserDto dto)
    {
        var result =  createUserUseCase.Handle(dto);
        result.ActionName = nameof(Create);
        return HandleResponse(result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "IsActive")]
    public ActionResult<UserOutDto> Update([FromRoute] int id, [FromBody] UpdateUserDto dto)
    {
        return HasAuthorization(id) ? HandleResponse(updateUserUseCase.Handle(id, dto)) : Forbid();
    }

    [HttpPut("role/{id:int}")]
    [Authorize(Policy = "IsAdmin")]
    public ActionResult<UserOutDto> UpdateRole([FromRoute] int id, [FromBody] UpdateRoleDto dto)
    {
        return HandleResponse(updateUserRoleUseCase.Handle(id, dto));
    }

    [HttpPost("disable/{id:int}")]
    [Authorize(Policy = "IsActive")]
    public ActionResult<bool> Disable([FromRoute] int id, [FromBody] DisableUserDto dto)
    {
        return HasAuthorization(id) ? HandleResponse(disableUserUseCase.Handle(id, dto)) : Forbid();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "IsActive")]
    public ActionResult<bool> Delete([FromRoute] int id)
    {
        return HasAuthorization(id) ? HandleResponse(deleteUserUseCase.Handle(id)) : Forbid();
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = "IsActive")]
    public ActionResult<UserOutDto> Get(int id)
    {
        return HasAuthorization(id) ? HandleResponse(getUserUseCase.Handle(id)) : Forbid();
    }

    [HttpGet("myself")]
    [Authorize(Policy = "IsActive")]
    public ActionResult<UserOutDto> Myself()
    {
        return HandleResponse(getUserUseCase.Handle(UserClaimsUtil.Id(User)));
    }

    // [HttpPost, Route("recovery")]
    // public ActionResult<bool>> Recovery([FromBody] RecoveryDto dto)
    // {
    //     return HandleResponse( sendRecoveryEmailUseCase.Handle(dto.Email));
    // }

    // [HttpPost, Route("verify")]
    // [Authorize(Policy = "IsActive")]
    // public ActionResult<bool>> Verify()
    // {
    //     return HandleResponse( sendRegisterEmailUseCase.Handle(UserClaims.Email(User)));
    // }

    // [HttpPost, Route("verify/{code}")]
    // [Authorize(Policy = "IsActive")]
    // public ActionResult<bool>> VerifyUser(string code)
    // {
    //     return HandleResponse( verifyUserUseCase.Handle(UserClaims.Id(User), UserClaims.Email(User), code));
    // }
}