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

    public async Task<List<UserPostsDto>> GetAllPostsByUser(Guid userId)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }
            var posts = await _context.ForumPosts.Where(p => p.User == user).ToListAsync();
            var result = new List<UserPostsDto>();
            foreach (var post in posts)
            {
                var postDto = new UserPostsDto()
                {
                    Id = post.Id,
                    User = post.User,
                    DiscussionThread = post.DiscussionThread,
                    Content = post.Content,
                    PostCreationDate = post.PostCreationDate,
                    Likes = post.Likes,
                };
                result.Add(postDto);
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