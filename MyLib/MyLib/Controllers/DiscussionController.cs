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
    public async Task<ActionResult<PostListResponseDto>> GetAllPosts()
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;
            var allPosts = await _discussionService.GetAllPostsAsync(username!);

            if (allPosts == null)
            {
                return Ok(new Response() { Message = "No posts found." });
            }

            var responseDto = new PostListResponseDto()
            {
                Posts = allPosts.ToList()
            };
            return Ok(responseDto);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred on the server." });
        }
    }
    
}