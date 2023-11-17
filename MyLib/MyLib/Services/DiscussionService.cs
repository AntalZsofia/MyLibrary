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
                Likes = 0,
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

    public async Task<ForumActionResult> CreateReplyAsync(CreateReplyDto createReplyDto, Guid postId, string username)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var newReply = new ForumReply()
            {

                Reply = createReplyDto.Reply,
                ReplyCreationDate = DateTime.UtcNow,
                Likes = 0,
                User = user,
                PostId = postId
            };
            await _context.ForumReplies.AddAsync(newReply);
            await _context.SaveChangesAsync();
            return ForumActionResult.Succeed("Reply created successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occured on the server");
        }

    }

    public async Task<PostDto> GetPostByIdAsync(string username, Guid id)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            var post = await _context.ForumPosts
                .Include(u => u.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                throw new Exception("There is no posts with this id");
            }

            var postFound = new PostDto()
            {
                Content = post.Content,
                DiscussionThread = post.DiscussionThread,
                Likes = post.Likes,
                PostCreationDate = post.PostCreationDate,
                User = post.User!,
                Id = id

            };
            return postFound;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occured on the server");
        }
    }

    public async Task<List<ReplyDto>> GetAllReplyToPostById(string username, Guid id)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            var replies = await _context.ForumReplies
                .Where(p => p.PostId == id)
                .Include(u => u.User)
                .ToListAsync();

            if (replies == null)
            {
                return new List<ReplyDto>();
            }

            var returnReplies = replies.Select(reply => new ReplyDto
            {
                Reply = reply.Reply,
                User = reply.User,
                PostCreationDate = reply.ReplyCreationDate,
                Likes = reply.Likes
            }).ToList();
            return returnReplies;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occured on the server");
        }
    }

    public async Task<UpdatePostResult> UpdatePostAsync(UpdatePostDto updatePostDto, Guid id, string username)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            var post = await _context.ForumPosts
                .Include(u => u.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return UpdatePostResult.Fail("Post with given id doesnt exists.");
            }


            post.Content = updatePostDto.Content;
            post.DiscussionThread = updatePostDto.DiscussionThread;
            post.PostCreationDate = updatePostDto.PostCreationDate;
            post.Likes = updatePostDto.Likes;
            post.User = user!;


            await _context.SaveChangesAsync();
            return UpdatePostResult.Success("Post successfully updated.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occured on the server");

        }
    }

    public async Task<ForumActionResult> DeletePostAsync(Guid id, string username)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            var post = await _context.ForumPosts
                .Include(u => u.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            var replies = await _context.ForumReplies
                .Include(r => r.User)
                .Where(r => r.PostId == id)
                .ToListAsync();
            
            if (post == null)
            {
                return ForumActionResult.Failed("Post with given id doesnt exists.");
            }

            _context.ForumPosts.Remove(post);
            _context.ForumReplies.RemoveRange(replies);
            await _context.SaveChangesAsync();
            return ForumActionResult.Succeed("Post successfully deleted.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        } 
    }
}