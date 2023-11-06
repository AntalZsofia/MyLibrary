using MyLib.Models.ResponseDto;

namespace MyLib.Models.Result;

public class ForumActionResult
{
    public bool Succeeded { get; set; }
    public Response Response { get; set; }

    public static ForumActionResult Succeed(string message)
    {
        var response = new Response() { Message = message };
        return new ForumActionResult() { Succeeded = true, Response = response};
    }

    public static ForumActionResult Failed(string message)
    {
        var response = new Response() { Message = message };
        return new ForumActionResult() { Succeeded = false, Response = response};

    }
}