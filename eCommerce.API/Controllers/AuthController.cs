using eCommerce.API.Extensions;
using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")] // api/auth
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IValidator<LoginRequest> _loginValidator; 
        private readonly IValidator<RegisterRequest> _registerValidator;

        public AuthController(IUsersService usersService, 
            IValidator<LoginRequest> loginValidator,
            IValidator<RegisterRequest> registerValidator)
        {
            _usersService = usersService;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpPost("login")] // api/auth/login

        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
             if(loginRequest == null)
            {
                return BadRequest("LoginRequest cannot be null");
            }

            var validationResult = await _loginValidator.ValidateAndReturnErrorsAsync(loginRequest);

            if (validationResult != null)
            {
                return validationResult;
            }

            AuthenticationResponse? authenticatedUser = await _usersService.LoginUser(loginRequest); 

            if(authenticatedUser == null || authenticatedUser.Success == false)
            {
                return Unauthorized(authenticatedUser);
            }

            return Ok(authenticatedUser);
        }

        [HttpPost("register")] // api/auth/register

        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                return BadRequest("RegisterRequest cannot be null");
            }

            var validationResult = await _registerValidator.ValidateAndReturnErrorsAsync(registerRequest);

            if (validationResult != null)
            {
                return validationResult;
            }

            AuthenticationResponse? registeredUser = await _usersService.RegisterUser(registerRequest);

            if (registeredUser == null || registeredUser.Success == false)
            {
                return BadRequest("Registration failed");
            }

            return Ok(registeredUser);
        }
    }
}
