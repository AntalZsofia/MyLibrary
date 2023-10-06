namespace MyLib.Models.ResponseDto;

public class LoginResponseDto
{
    public IList<string> Roles { get; set; }
    public string Username { get; set; }
}