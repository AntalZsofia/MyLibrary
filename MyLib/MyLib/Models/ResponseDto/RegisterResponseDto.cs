using MyLib.Models.RequestDto;

namespace MyLib.Models.ResponseDto;

public class RegisterResponseDto
{
    public List<string> Message { get; set; } = new();
    public RegisterUserDto? User { get; set; }
}