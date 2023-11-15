using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
}