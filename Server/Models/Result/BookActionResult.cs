using MyLibrary.Server.Models.ResponseDto;

namespace MyLibrary.Server.Models.Result;

public class BookActionResult
{
    public bool Succeeded { get; set; }
        public Response Response { get; set; }

        public static BookActionResult Succeed(string message)
        {
            var response = new Response() { Message = message };
            return new BookActionResult() { Succeeded = true, Response = response};
        }

        public static BookActionResult Failed(string message)
        {
            var response = new Response() { Message = message };
            return new BookActionResult() { Succeeded = false, Response = response};

        }
    
}