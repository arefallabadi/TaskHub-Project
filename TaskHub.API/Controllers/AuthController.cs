using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHub.API.DTOs.Auth;
using TaskHub.API.Services.Interfaces;

namespace TaskHub.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authService.Login(dto);
            if (result == null)
                return Unauthorized(new { Message = "Invalid username or password" });

            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authService.Register(dto);
            if (result == null)
                return BadRequest(new { Message = "Username already exists or registration failed" });

            return Ok(result);
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public IActionResult SystemAuth([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_authService.ValidateSystemCredentials(dto.Username, dto.Password))
                return Unauthorized();

            // System auth generates token with Admin role and userId 0 (system user)
            var token = _authService.GenerateToken(dto.Username, "Admin", 0);
            return Ok(new { token });
        }
    }
}
