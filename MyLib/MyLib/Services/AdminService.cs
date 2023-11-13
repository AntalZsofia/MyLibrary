using Microsoft.EntityFrameworkCore;
using MyLib.Models;
using MyLib.Models.Entities;

namespace MyLib.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;

    public AdminService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        try
        {
            var users = await _context.Users.ToListAsync();
            return users;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}