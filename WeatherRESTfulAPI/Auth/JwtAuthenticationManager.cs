using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtAuthenticationManager: IJwtAuthenticationManager
{
    private readonly string _key;

    public JwtAuthenticationManager(string key)
    {
        _key = key;
    }

    public string GenerateToken(UserDto user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenKey = Encoding.ASCII.GetBytes(_key);

        var tokenDescriptior = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
            IssuedAt = DateTime.Now,
            Expires = DateTime.Now.AddHours(2),
            SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptior);

        return tokenHandler.WriteToken(token);

    }
}
