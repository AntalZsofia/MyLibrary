using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLib.Models;
using MyLib.Models.Entities;
using MyLib.Models.RequestDto;
using MyLib.Models.Result;
using Exception = System.Exception;

namespace MyLib.Services;

public class DiscussionService : IDiscussionService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly HttpClient _httpClient = new HttpClient();

    public DiscussionService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<ForumActionResult> CreatePostAsync(CreatePostDto createPostDto, string username)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);

            var newForumPost = new ForumPost()
            {
                Content = createPostDto.Content,
                DiscussionThread = createPostDto.DiscussionThread,
                PostCreationDate = DateTime.UtcNow,
                User = user!
                
            };
            await _context.ForumPosts.AddAsync(newForumPost);
            await _context.SaveChangesAsync();
            return ForumActionResult.Succeed("Post created successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occured on the server");
        }
    }

    public async Task<IEnumerable<ForumPost>> GetAllPostsAsync(string username)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var allPosts = await _context.ForumPosts
                .Include(u => u.User)
                .ToListAsync();

            if (allPosts == null || !allPosts.Any())
            {
                return new List<ForumPost>();
            }

            return allPosts;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred on the server");
        }
    }
}