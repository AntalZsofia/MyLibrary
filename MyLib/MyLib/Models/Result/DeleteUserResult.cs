using MyLib.Models.Enums;

namespace MyLib.Models.Result;

public class DeleteUserResult
{
    public bool Succeeded { get; set; }
    public ErrorType? Error { get; set; }
    public string Message { get; set; } = string.Empty;

    public DeleteUserResult(bool isSuccessful)
    {
        Succeeded = isSuccessful;
    }

    public static DeleteUserResult UserNotFound()
    {
        var result = new DeleteUserResult(false)
        {
            Error = ErrorType.UserNotFound,
            Message = "Couldn't find user."
        };
        return result;
    }
    
    public static DeleteUserResult Success()
    {
        var result = new DeleteUserResult(true)
        {
            Message = "Deleted user."
        };
        return result;
    }

    public static DeleteUserResult ServerError()
    {
        var result = new DeleteUserResult(false)
        {
            Error = ErrorType.Server,
            Message = "Couldn't delete user due to server error."
        };
        return result;
    }
}