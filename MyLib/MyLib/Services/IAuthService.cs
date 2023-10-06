using MyLib.Models.RequestDto;
using MyLib.Models.Result;


namespace MyLib.Services;

public interface IAuthService
{
    Task<RegisterResult> RegisterAsync(RegisterUserDto registerUserDto);
    Task<LoginResult> LoginAsync(LoginUserDto loginUserDto);
    Task<IList<string>> GetRolesAsync(string userName);
}