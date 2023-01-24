using WeatherRESTfulAPI.DTOs;
using WeatherRESTfulAPI.Services.Interface;

namespace WeatherRESTfulAPI.Services.Implementation
{
    public class UserService : IUserService
    {

        public static List<User> Users = new List<User>();
        
        public BaseResponse<UserDto> Login(LoginUserDTO model)
        {

            var user = Users.FirstOrDefault(u => u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase) && u.Password.Equals(model.Password));

            if (user == null) return new BaseResponse<UserDto>
            {
                Message = "Invalid Email Or Password"
            };

            return new BaseResponse<UserDto>
            {
                Success = true,
                Message = "Login Successful",
                Data = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email
                }
            };

        }

        public BaseResponse<UserDto> Register(RegisterUserDTO model)
        {
            var isUserExist = Users.Exists(u => u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase));

            if (isUserExist) return new BaseResponse<UserDto>
            {
                Message = "User Already Exists"
            };

            User user = new User
            {
                Id = Users.Count == 0 ? 1 : Users.Last().Id + 1,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password
            };



            Users.Add(user);

            return new BaseResponse<UserDto>
            {
                Success = true,
                Message = "User registered Successfully",
                Data = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                }
            };
        }
    }
}
