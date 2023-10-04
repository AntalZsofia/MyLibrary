using Microsoft.AspNetCore.Mvc;
using MyLibrary.Server.Models.RequestDto;
using MyLibrary.Server.Models.ResponseDto;
using MyLibrary.Server.Models.Result;
using MyLibrary.Server.Services;

namespace MyLibrary.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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
    [Route(("api/login"))]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(LoginResult.Fail("Invalid inputs"));
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

                var roles = await _authService.GetRolesAsync(loginUserDto.UserName!);

                return Ok(new LoginResponseDto
                {
                    Roles = roles,
                    UserName = loginUserDto.UserName
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
}