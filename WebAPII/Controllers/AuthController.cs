using Business.Abstract;
using Entity.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExist = _authService.UserExist(userForRegisterDto.Email);
            if (!userExist.success)
            {
                return BadRequest(userExist.message);
            }
            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (!result.success)
            {
                return BadRequest(result.message);
            }
            return Ok(result);
        }
        [HttpGet("getuser")]
        public IActionResult GetUser(int userId)
        {
            var result = _authService.GetUser(userId);
            if (result.success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
      [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.success)
            {
                return BadRequest(userToLogin.message);
            }
            
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (!result.success)
            {
                return BadRequest(result.message);
            }
            return Ok(result);
        }
      
    }
}
