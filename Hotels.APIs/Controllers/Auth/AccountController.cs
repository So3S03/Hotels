using Hotels.APIs.Controllers.Base;
using Hotels.Application.AuthModule.ActivationFeature.Command;
using Hotels.Application.AuthModule.LoginFeature.Command;
using Hotels.Application.AuthModule.RegisterFeature.Command;
using Hotels.Application.AuthModule.UsersGridFeature.Query;
using Hotels.Shared.Dtos._Common;
using Hotels.Shared.Dtos.AuthModule;
using Hotels.Shared.Dtos.AuthModule._Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.APIs.Controllers.Auth
{
    public class AccountController(IMediator _mediator) : BaseApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<SignInStatusDto>> Login(LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ActionStatusDto>> Register(RegisterCommand registerCommand)
        {
            var result = await _mediator.Send(registerCommand);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("ActivateDeactivateUser")]
        public async Task<ActionResult<ActionStatusDto>> ActivateDeactivateUser(ActivationCommand activationCommand)
        {
            var result = await _mediator.Send(activationCommand);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<GridsToReturnDto<UserToReturnDto>>> GetAllUsers([FromQuery] UsersQuery usersQuery)
        {
            var result = await _mediator.Send(usersQuery);
            return Ok(result);
        }
    }
}
