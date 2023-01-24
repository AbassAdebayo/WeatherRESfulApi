using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeatherRESTfulAPI.Services.Interface;

namespace WeatherRESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public UserController(IUserService userService, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _userService = userService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }


        [HttpPost("register")]
        public IActionResult Register(RegisterUserDTO model)
        {

            var response = _userService.Register(model);

            if (response.Success) return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDTO model)
        {

            var response = _userService.Login(model);

            if (!response.Success) return BadRequest(response);

            var token = _jwtAuthenticationManager.GenerateToken(response.Data);

            var user = response.Data;

            var r = new LoginResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };

            return Ok(r);
        }


    }
}
