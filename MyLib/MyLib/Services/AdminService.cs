using Microsoft.EntityFrameworkCore;
using MyLib.Models;
using MyLib.Models.Entities;
using MyLib.Models.ResponseDto;

namespace MyLib.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;

    public AdminService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UsersResponseDto>> GetAllUsersAsync()
    {
        try
        {
            var users = await _context.Users.ToListAsync();
            var result = new List<UsersResponseDto>();

            foreach (var user in users)
            {
                var userDto = new UsersResponseDto()
                {
                    Email = user.Email,
                    ProfileCreationDate = user.ProfileCreationDate,
                    Username = user.UserName
                };
            result.Add(userDto);
            }

            return result;


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}