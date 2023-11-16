using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyLib.Models.Entities;
using MyLib.Models.ResponseDto;
using MyLib.Services;

namespace MyLib.Controllers;

    [EnableCors("_myAllowSpecificOrigins")]
    [Authorize(Roles="Admin")]
    [Microsoft.AspNetCore.Components.Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }


    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!HttpContext.Request.IsHttps)
            {
                return BadRequest();
            }

            var result = await _adminService.GetAllUsersAsync();

            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("/allposts/{id}")]
    public async Task<IActionResult> GetAllPostsByUser(Guid id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _adminService.GetAllPostsByUserAsync(id);
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred on the server." });
        }
    }
    
    [HttpDelete("/deletepost/{postId}")]
    public async Task<IActionResult>DeletePostByUser(Guid postId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _adminService.GetUserByPostIdAsync(postId);
            var result = await _adminService.DeletePostAndRepliesByUserAsync(user!.UserName, postId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred on the server." });
        }
    }

    [HttpDelete("/deleteuser/{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _adminService.DeleteUserAsync(userId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}