using MyLib.Models.ResponseDto;

namespace MyLib.Models.Result;

public class ReadingStatusResult
{
    public bool Succeeded { get; set; }
    public Response Response { get; set; }
    
    public static ReadingStatusResult Succeed(string message)
    {
        var response = new Response() { Message = message };
        return new ReadingStatusResult() { Succeeded = true, Response = response};
    }
    
    public static ReadingStatusResult Failed(string message)
    {
        var response = new Response() { Message = message };
        return new ReadingStatusResult() { Succeeded = false, Response = response};
        
    }
    
}