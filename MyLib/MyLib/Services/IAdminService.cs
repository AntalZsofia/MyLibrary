using MyLib.Models.Entities;
using MyLib.Models.ResponseDto;

namespace MyLib.Services;

public interface IAdminService
{
    Task<List<UsersResponseDto>> GetAllUsersAsync();
    Task<List<UserPostsDto>> GetAllPostsByUser(Guid userId);
}