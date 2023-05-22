using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using System.Text;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using Zadanie8.Dto;
using Zadanie8.Models;

namespace Zadanie8.Services.ServicesIml;

public class LoginServiceImpl : ILogginService
{
    private readonly MainDbContext _mainDbContext;

    private readonly IUserService _userService;

    public LoginServiceImpl(MainDbContext mainDbContext, IUserService userService)
    {
        _mainDbContext = mainDbContext;
        _userService = userService;
    }

    public async Task<LoginResult> LoginUser(LogginDtoRequest logginDtoRequest)
    {
        var user = await _userService.GetUserByEmail(logginDtoRequest.Email);
        if (user == null)
        {
            throw new Exception($"No user with email {logginDtoRequest.Email}");
        }

        string passwordFromUser = logginDtoRequest.Password;
        byte[] storedSalt = user.Salt;
        byte[] storedHashedPassword = user.PasswordHash;

        bool passwordsMatch = ComparePasswords(passwordFromUser, storedSalt, storedHashedPassword);

        if (!passwordsMatch)
        {
            throw new Exception("The password incorrect!!!");
        }

        return GenerateJwtToken(user.Email);
    }
    
    
    private bool ComparePasswords(string userPassword, byte[] storedSalt, byte[] storedHash)
    {
        byte[] userHash = UserServiceImpl.HashPassword(userPassword, storedSalt);

        return userHash.SequenceEqual(storedHash);
    }
    
    
    public LoginResult GenerateJwtToken(string userEmail)
    {
        var claims = new Claim[]
        {
            new(ClaimTypes.Email, userEmail),
            new("Custom", "SomeData"),
            new Claim(ClaimTypes.Role, "admin")
        };

        var secret = "ahahahahahahahahhahahahahhahahahhahahahahhahahahhahahahhahahahhahahahahahhahahahhahahahahahahahhahahahahhahahahahaha";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var options = new JwtSecurityToken("https://localhost:5001", "https://localhost:5001",
            claims, expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: creds);

        var refreshToken = "";
        using (var genNum = RandomNumberGenerator.Create())
        {
            var r = new byte[1024];
            genNum.GetBytes(r);
            refreshToken = Convert.ToBase64String(r);
        }

        return new LoginResult
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(options),
            RefreshToken = refreshToken
        };
    }
    
    
    

}

