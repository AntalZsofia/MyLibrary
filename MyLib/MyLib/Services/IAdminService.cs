using MyLib.Models.Entities;
using MyLib.Models.ResponseDto;
using MyLib.Models.Result;

namespace MyLib.Services;

public interface IAdminService
{
    Task<List<UsersResponseDto>> GetAllUsersAsync();
    Task<List<UserPostsDto>> GetAllPostsByUserAsync(Guid userId);
    Task<DeletePostResult> DeletePostAndRepliesByUserAsync(Guid userId, Guid postId);
    Task<DeleteUserResult>DeleteUserAsync(Guid userId);
}