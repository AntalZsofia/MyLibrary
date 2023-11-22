using MyLib.Models.ResponseDto;

namespace MyLib.Models.Result;

public class ReviewResult
{
    public bool Succeeded { get; set; }
    public Response Response { get; set; }

    public static ReviewResult Succeed(string reviewAddedSuccessfully)
    {
        var response = new Response() { Message = reviewAddedSuccessfully };
        return new ReviewResult() { Succeeded = true, Response = response};
    }
}