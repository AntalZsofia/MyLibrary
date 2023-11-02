using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyLib.Models;
using MyLib.Models.Entities;
using MyLib.Models.RequestDto;
using MyLib.Models.Result;


namespace MyLib.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly ApplicationDbContext _context;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole<Guid>> roleManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
        _context = context;
    }
    
    //Register
    public async Task<RegisterResult> RegisterAsync(RegisterUserDto registerUserDto)
    {
        try
        {
            var user = new ApplicationUser()
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Email,
                ProfileCreationDate = registerUserDto.ProfileCreationDate
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

    public async Task<ApplicationUser> GetUserByUsername(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return user!;
    }

    public async Task<UpdateProfileResult> UpdateUserAsync(UpdateProfileDto updateProfileDto, string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null)
        {
            return UpdateProfileResult.Fail("There is no user with this id");
            
        }
        if(updateProfileDto.Username != null)
        {
            var result = await _userManager.SetUserNameAsync(user, updateProfileDto.Username);
            if (!result.Succeeded)
            {
                return UpdateProfileResult.Fail("Username change was not successful");
            }
        }
        if(updateProfileDto.Email != null){
       var result = await _userManager.SetEmailAsync(user, updateProfileDto.Email);
        if (!result.Succeeded)
        {
            return UpdateProfileResult.Fail("Email change was not successful");
        }
        }
       
        if (updateProfileDto.NewPassword != null && updateProfileDto.OldPassword != null)
        {
            var result = await _userManager.ChangePasswordAsync(user, updateProfileDto.OldPassword,
                    updateProfileDto.NewPassword);
            if (!result.Succeeded)
            {
                return UpdateProfileResult.Fail("Password change was not successful");
            }
        }

        await _context.SaveChangesAsync();
        return UpdateProfileResult.Success("Profile updated successfully");
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