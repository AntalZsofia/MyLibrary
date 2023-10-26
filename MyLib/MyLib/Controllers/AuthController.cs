using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyLib.Models.RequestDto;
using MyLib.Models.ResponseDto;
using MyLib.Models.Result;
using MyLib.Services;

namespace MyLib.Controllers;
[EnableCors("_myAllowSpecificOrigins")]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IBookService _bookService;

    public AuthController(IAuthService authService, IBookService bookService)
    {
        _authService = authService;
        _bookService = bookService;
    }
    
    //Registration
    [HttpPost]
    [Route("/api/signup")]
    public async Task<IActionResult> Signup([FromBody]RegisterUserDto registerUserDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors.Select(err => err.ErrorMessage));
                return BadRequest(new { Errors = errors });
            }
            
            var registrationResult = await _authService.RegisterAsync(registerUserDto);

            if (registrationResult.Succeeded)
            {
                return Ok(new RegisterResponseDto
                {
                    Message = registrationResult.Message,
                    User = registerUserDto
                });
            }

            return BadRequest(new RegisterResponseDto { Message = registrationResult.Message });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response { Message = "An error occured on the server." });
        }


    }
    //Login
    [HttpPost]
    [Route(("/api/login"))]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(LoginResult.Fail("Invalid inputs"));
            }
            // Check if the request is made over HTTPS
            if (!HttpContext.Request.IsHttps)
            {
                return BadRequest(LoginResult.Fail("This endpoint requires a secure (HTTPS) connection."));
            }

            var authResult = await _authService.LoginAsync(loginUserDto);
            if (authResult.Succeeded)
            {
                HttpContext.Response.Cookies.Append("token", authResult.Token, new CookieOptions()
                {
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.Now.AddDays(14),
                    IsEssential = true,
                    Secure = true,
                    HttpOnly = true
                });

                var roles = await _authService.GetRolesAsync(loginUserDto.Username!);

                return Ok(new LoginResponseDto
                {
                    Roles = roles,
                    Username = loginUserDto.Username
                });
            }

            return Unauthorized(new LoginResult { Succeeded = false, ErrorMessage = "Wrong username or password." });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500,
                new LoginResult { Succeeded = false, ErrorMessage = "An error occured on the server." });
        }
} 
    [Authorize]
    [HttpPost]
    [Route("/api/logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            HttpContext.Response.Cookies.Append("token", "", new CookieOptions()
            {
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.Now.AddDays(-1400),
                IsEssential = true,
                Secure = true,
                HttpOnly = true
            });
            return Ok("Signed out successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "An error occurred on the server.");
        }
    }
    
    //User Profile
    [Authorize]
    [HttpGet("/api/user/me")]
     public async Task<IActionResult> GetProfile()
     {
         var username = User.Identity?.Name;
         var user = await _authService.GetUserByUsername(username!);
         var booksCount = await _bookService.GetUserBookCount(username!);
    
         if (user == null)
         {
             return NotFound("User not found");
         }
    
         return Ok(new UserProfileDto
         {
             Username = user.UserName,
             Email = user.Email,
             Password = user.PasswordHash,
             ProfileCreationDate = user.ProfileCreationDate,
             BooksCount = booksCount
             
         });
     }

     [Authorize]
     [HttpPut("/api/user/me")]
     public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfileDto)
     {
         try
         {
         var username = User.Identity?.Name;
         var user = await _authService.GetUserByUsername(username);
         if (user == null)
         {
             return NotFound("User not found"); 
         }

         var updateProfileResult = await _authService.UpdateUserAsync(updateProfileDto, username);
         if (updateProfileResult.Succeeded)
         {
             return Ok(updateProfileResult.Message);
         }
         return BadRequest(updateProfileResult.Message);

         }
         
        catch(Exception e)
         {
         Console.WriteLine(e);
         return StatusCode(500, new Response(){Message = "An error occured on the server."});
         }
     }
     }
