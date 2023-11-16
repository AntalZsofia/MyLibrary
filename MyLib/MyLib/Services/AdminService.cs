using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLib.Models;
using MyLib.Models.Entities;
using MyLib.Models.ResponseDto;
using MyLib.Models.Result;

namespace MyLib.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
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
                    User = user
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

    public async Task<List<UserPostsDto>> GetAllPostsByUserAsync(Guid userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var posts = await _context.ForumPosts
                .Where(p => p.User == user)
                .Include(forumPost => forumPost.User!)
                .ToListAsync();
            var result = new List<UserPostsDto>();
            foreach (var post in posts)
            {
                var postDto = new UserPostsDto()
                {
                    Id = post.Id,
                    User = post.User!,
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

    public async Task<DeletePostResult> DeletePostAndRepliesByUserAsync(string username, Guid postId)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            var post = await _context.ForumPosts
                .Include(p => p.User)
                .Where(p => p.User == user)
                .FirstOrDefaultAsync(p => p.Id == postId);

            var replies = await _context.ForumReplies
                .Include(r => r.User)
                .Where(r => r.PostId == postId)
                .ToListAsync();
            if (user == null)
            {
                return DeletePostResult.UserNotFound();
            }

            if (post == null)
            {
                return DeletePostResult.PostNotFound();
            }
            
            _context.ForumPosts.Remove(post);
            _context.ForumReplies.RemoveRange(replies);
            await _context.SaveChangesAsync();
            return DeletePostResult.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<DeleteUserResult> DeleteUserAsync(Guid userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return DeleteUserResult.UserNotFound();
            }
            await _userManager.DeleteAsync(user);
            await _context.SaveChangesAsync();
            return DeleteUserResult.Success();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ApplicationUser?> GetUserByPostIdAsync(Guid postId)
    {
        try
        {
            var post = _context.ForumPosts
                .Include(p => p.User)
                .FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                return null;
            }

            return await Task.FromResult(post.User);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

