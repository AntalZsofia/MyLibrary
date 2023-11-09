using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyLib.Models.RequestDto;
using MyLib.Models.ResponseDto;
using MyLib.Services;

namespace MyLib.Controllers;
[EnableCors("_myAllowSpecificOrigins")]
    [Authorize]//(Roles = "User")          
    [ApiController]
    [Route("[controller]")]

public class DiscussionController : ControllerBase
{
    private readonly IDiscussionService _discussionService;
    private readonly IConfiguration _configuration;

    public DiscussionController(IDiscussionService discussionService, IConfiguration configuration)
    {
        _discussionService = discussionService;
        _configuration = configuration;
    }
    
    //Create new post
    [HttpPost("/create-post")]
    public async Task<ActionResult> CreatePost(CreatePostDto createPostDto)
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;

            var createPostResult = await _discussionService.CreatePostAsync(createPostDto, username);

            if (createPostResult.Succeeded)
            {
                return Ok(createPostResult.Response);
            }
            else
            {
                return BadRequest(new Response() { Message = "Could not create new post" });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response(){Message = "An error occured on the server."});
        }
    }
    
    //Get all posts
    [HttpGet("/get-all-posts")]
    public async Task<ActionResult<PostListResponsePreviewDto>> GetAllPosts()
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;
            var allPosts = await _discussionService.GetAllPostsAsync(username!);

            if (allPosts == null)
            {
                return Ok(new Response() { Message = "No posts found." });
            }

            var responseDto = new PostListResponsePreviewDto()
            {
                Posts = allPosts.Select(post => new PostResponsePreviewDto
                {
                    ContentPreview = TruncateContent(post.Content, 50),
                    Username = post.User!.UserName,
                    DiscussionThread = post.DiscussionThread,
                    PostCreationDate = post.PostCreationDate,
                    Likes = post.Likes,
                    Id = post.Id
                }).ToList()
            };
            return Ok(responseDto);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred on the server." });
        }
    }
    //Get post by id
    [HttpGet("/get-post/{id}")]
    public async Task<IActionResult> GetPostById(Guid id)
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;

            var post = await _discussionService.GetPostByIdAsync(username!, id);
            

            if (post == null)
            {
                return NotFound();
            }
          
            return Ok(post);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred on the server." });
        }
    }

    //Get replies to postId
    [HttpGet("/get-replies/{id}")]
        public async Task<IActionResult> GetRepliesByPostId(Guid id)
        {
            try
            {
                var username = HttpContext.User.Identity!.Name;
            
                var replies = await _discussionService.GetAllReplyToPostById(username!, id);
            
                if (replies != null)
                {
                    return Ok(replies);
                }

                return Ok(new List<ReplyDto>());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, new Response() { Message = "An error occurred on the server." });
            }
    }
    
    
    //Create a reply
    [HttpPost("/create-reply/{id}")]
    public async Task<ActionResult> CreateReply(CreateReplyDto createReplyDto, Guid id)
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;
            var createReplyResult = await _discussionService.CreateReplyAsync(createReplyDto, id, username!);

            if (createReplyResult.Succeeded)
            {
                return Ok(createReplyResult.Response);
            }

            return BadRequest(new Response() { Message = "Could not create new reply" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response(){Message = "An error occured on the server."});
        }
    }
    private string TruncateContent(string content, int maxLength)
    {
        if (content.Length <= maxLength)
        {
            return content;
        }

        // Find the last space within the character limit
        int lastSpace = content.LastIndexOf(' ', maxLength);

        // Truncate the content at the last space or maxLength
        string truncatedContent = lastSpace > 0 ? content.Substring(0, lastSpace) : content.Substring(0, maxLength);

        // Add an ellipsis (...) to indicate truncation
        return truncatedContent + " ...";
    }
    




}