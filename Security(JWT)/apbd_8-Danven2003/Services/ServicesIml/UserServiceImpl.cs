using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Zadanie8.Dto;
using Zadanie8.Models;

namespace Zadanie8.Services.ServicesIml;

public class UserServiceImpl : IUserService
{

    private readonly MainDbContext _mainDbContext;

    public UserServiceImpl(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }

    public async Task<string> CreateUser(UserDtoRequest userDtoRequest)
    {
        var user = MapToUserFromRequest(userDtoRequest);

        byte[] salt = GenerateSalt();

        byte[] passwordHash = HashPassword(userDtoRequest.Password, salt);

        user.Salt = salt;
        user.PasswordHash = passwordHash;
        
        _mainDbContext.Users.Add(user);
        await _mainDbContext.SaveChangesAsync();
        return "generated successfully";
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _mainDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }

    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    public static byte[] HashPassword(string password, byte[] salt)
    {
        int iterations = 10000; // Number of iterations
        int saltSize = 128 / 8; // Salt size in bytes
        int hashSize = 256 / 8; // Hash size in bytes

        byte[] hashedPassword = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: iterations,
            numBytesRequested: hashSize + saltSize
        );

        byte[] extractedSalt = new byte[saltSize];
        byte[] extractedHash = new byte[hashSize];
        Buffer.BlockCopy(hashedPassword, 0, extractedSalt, 0, saltSize);
        Buffer.BlockCopy(hashedPassword, saltSize, extractedHash, 0, hashSize);

        return extractedHash;
    }

    private User MapToUserFromRequest(UserDtoRequest userDtoRequest)
    {
        return new User
        {
            Email = userDtoRequest.Email
            
        };
    }
}