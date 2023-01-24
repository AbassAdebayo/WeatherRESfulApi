using System;

public interface IJwtAuthenticationManager
{
    public string GenerateToken(UserDto user);
}
