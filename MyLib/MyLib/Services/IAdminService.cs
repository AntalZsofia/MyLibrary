using MyLib.Models.Entities;

namespace MyLib.Services;

public interface IAdminService
{
    Task<List<ApplicationUser>> GetAllUsersAsync();
}