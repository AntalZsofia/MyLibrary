using MyLibrary.Server.Models.RequestDto;
using MyLibrary.Server.Models.Result;

namespace MyLibrary.Server.Services;

public interface IAuthService
{
    Task<RegisterResult> RegisterAsync(RegisterUserDto registerUserDto);
    Task<LoginResult> LoginAsync(LoginUserDto loginUserDto);
    Task<IList<string>> GetRolesAsync(string userName);
}