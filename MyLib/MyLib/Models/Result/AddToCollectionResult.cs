using MyLib.Models.ResponseDto;

namespace MyLib.Models.Result;

public class AddToCollectionResult
{
    public bool Succeeded { get; set; }
    public Response Response { get; set; }

    public static AddToCollectionResult Succeed(string message)
    {
        var response = new Response() { Message = message };
        return new AddToCollectionResult() { Succeeded = true, Response = response};
    }

    public static AddToCollectionResult Failed(string message)
    {
        var response = new Response() { Message = message };
        return new AddToCollectionResult() { Succeeded = false, Response = response};

    }
}