using MyLib.Models.Enums;

namespace MyLib.Models.Result;

public class DeletePostResult
{
    public bool Succeeded { get; set; }
    public ErrorType? Error { get; set; }
    public string Message { get; set; } = string.Empty;

    public DeletePostResult(bool isSuccessful)
    {
        Succeeded = isSuccessful;
    }

    public static DeletePostResult PostNotFound()
    {
        var result = new DeletePostResult(false)
        {
            Error = ErrorType.PostNotFound,
            Message = "Post not found."
        };

        return result;
    }

    public static DeletePostResult UserNotFound()
    {
        var result = new DeletePostResult(false)
        {
            Error = ErrorType.UserNotFound,
            Message = "Couldn't find user."
        };
        return result;
    }
    public static DeletePostResult RepliesNotFound()
    {
        var result = new DeletePostResult(false)
        {
            Error = ErrorType.RepliesNotFound,
            Message = "Couldn't find replies."
        };
        return result;
    }

    public static DeletePostResult Success()
    {
        var result = new DeletePostResult(true)
        {
            Message = "Deleted post."
        };
        return result;
    }

    public static DeletePostResult ServerError()
    {
        var result = new DeletePostResult(false)
        {
            Error = ErrorType.Server,
            Message = "Couldn't delete post due to server error."
        };
        return result;
    }
}