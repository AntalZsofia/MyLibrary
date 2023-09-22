﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyLibrary.Server.Models.Entities;
using MyLibrary.Server.Models.RequestDto;
using MyLibrary.Server.Models.Result;


namespace MyLibrary.Server.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
   
    //Login
    public async Task<LoginResult> LoginAsync(LoginUserDto loginUserDto)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(loginUserDto.UserName);
            if (user != null)
            { 
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
                if (isPasswordValid)
                {
                    var token = await GetJwtTokenAsync(user, loginUserDto.Password!);
                    return LoginResult.Success(token!);
                }
            }
            return LoginResult.Fail("Wrong username or password.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occured on the server.");
        }
    }

    public Task<IList<string>> GetRolesAsync(string userName)
    {
        throw new NotImplementedException();
    }
    
    
    public async Task<string> GetJwtTokenAsync(ApplicationUser user, string password)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        var roles = await _userManager.GetRolesAsync(user);
        
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience:_configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(14),
            claims:claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}