using MyLibrary.Server.Models.RequestDto;

namespace MyLibrary.Server.Models.ResponseDto;

public class RegisterResponseDto
{
    public List<string> Message { get; set; } = new();
    public RegisterUserDto? User { get; set; }
}