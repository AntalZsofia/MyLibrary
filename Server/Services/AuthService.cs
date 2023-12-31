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
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }
    
    //Register
    public async Task<RegisterResult> RegisterAsync(RegisterUserDto registerUserDto)
    {
        try
        {
            var user = new ApplicationUser()
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Email
            };
            await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));
            var registerResult = await _userManager.CreateAsync(user, registerUserDto.Password);
            var asignRoleResult = await _userManager.AddToRoleAsync(user, "User");

            if (registerResult.Succeeded && asignRoleResult.Succeeded)
            {
                return (new RegisterResult()
                {
                    Succeeded = true,
                    Message = new List<string>
                    {
                        "Registration successful"
                    }
                });
            }

            if (registerResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
            }

            return (new RegisterResult()
            {
                Succeeded = false,
                Message = registerResult.Errors.Select(e => e.Description).ToList()
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    //Login
    public async Task<LoginResult> LoginAsync(LoginUserDto loginUserDto)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(loginUserDto.Username);
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

    public async Task<IList<string>> GetRolesAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        var roles = await _userManager.GetRolesAsync(user);
        return roles;
    }


    private async Task<string> GetJwtTokenAsync(ApplicationUser user, string password)
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
        
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHighlySecuredSecretKey12345"));
        
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience:_configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims:claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}