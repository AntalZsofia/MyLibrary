using MyLib.Models.Entities;
using MyLib.Models.RequestDto;
using MyLib.Models.Result;


namespace MyLib.Services;

public interface IAuthService
{
    Task<RegisterResult> RegisterAsync(RegisterUserDto registerUserDto);
    Task<LoginResult> LoginAsync(LoginUserDto loginUserDto);
    Task<IList<string>> GetRolesAsync(string userName);
    Task<ApplicationUser> GetUserByUsername(string username);
    Task<UpdateProfileResult> UpdateUserAsync(UpdateProfileDto updateProfileDto, string username);
}