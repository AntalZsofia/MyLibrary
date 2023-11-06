using Microsoft.AspNetCore.Identity;
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
}