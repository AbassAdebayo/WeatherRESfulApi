using WeatherRESTfulAPI.DTOs;

namespace WeatherRESTfulAPI.Services.Interface
{
    public interface IUserService
    {
        public BaseResponse<UserDto> Login(LoginUserDTO model);

        public BaseResponse<UserDto> Register(RegisterUserDTO model);

    }
}
