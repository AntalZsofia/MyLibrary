using MyLib.Models.Entities;
using MyLib.Models.ResponseDto;
using MyLib.Models.Result;

namespace MyLib.Services;

public interface IAdminService
{
    Task<List<UsersResponseDto>> GetAllUsersAsync();
    Task<List<UserPostsDto>> GetAllPostsByUserAsync(Guid userId);
    Task<DeletePostResult> DeletePostAndRepliesByUserAsync(string username, Guid postId);
    Task<DeleteUserResult>DeleteUserAsync(Guid userId);
    Task<ApplicationUser?> GetUserByPostIdAsync(Guid postId);
}