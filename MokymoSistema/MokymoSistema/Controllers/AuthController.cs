using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MokymoSistema.DTO;
using MokymoSistema.Models;
using MokymoSistema.Services;

namespace MokymoSistema.Controllers
{
    [Route("api/")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(UserManager<User> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByNameAsync(registerDto.Username);
            if (user != null)
                return BadRequest(new {Error = "User with this name already exists"});

            var newUser = new User { UserName = registerDto.Username, Email = registerDto.Email };
            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!createUserResult.Succeeded)
            {
                return BadRequest(new { Error = createUserResult.Errors.First() });
            }

            await _userManager.AddToRoleAsync(newUser, UserRoles.Lecturer);

            return CreatedAtAction(nameof(Register),
                new UserDto { Email = newUser.Email, Id = newUser.Id, Username = newUser.UserName });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
                return BadRequest(new { Error = "Username or password is invalid" });

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return BadRequest(new { Error = "Username or password is invalid" });

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);

            return Ok(new loginResponseDto { Token = accessToken });
        }

        [HttpGet]
        [Route("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }


    }
}
