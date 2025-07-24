using Application.Account.DTOs;
using Application.Account.Commands;
using Infrastructure.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Account.Handlers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
           _mediator = mediator;
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterDto model)
        {

            if(model.Password != model.PasswordConfirm)
            {
                return BadRequest("Passwords do not match");
            }

            var result = await _mediator.Send(new RegisterCommand(model.Email, model.PhoneNumber, model.FullName, model.Password));

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var result = await _mediator.Send(new LoginCommand(model.Identifier, model.Password));       

            if(!result.Succeeded)
            {
                return Unauthorized(result.Errors);
            }

            return Ok(result);
        }
    }
}
