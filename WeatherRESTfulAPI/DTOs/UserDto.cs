using System;

public class UserDto
{
	public int Id { get; set; }

	public string Email { get; set; }

	public string FirstName { get; set; }

	public string LastName { get; set; }

	public string password { get; set; }


	

}

  public class RegisterUserDTO
 {
	public string FirstName { get; set; }

	public string LastName { get; set; }

	public string Email { get; set; }

	public string Password { get; set; }

	public string ConfirmPassword { get; set; }
  }

public class LoginResponseDto : UserDto
	{
		public string Token { get; set; }
	}
	public class LoginUserDTO
	{
	public string Email { get; set; } 

	public string Password { get; set; } 
	}
