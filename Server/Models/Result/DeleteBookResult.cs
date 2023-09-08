using MyLibrary.Server.Models.Enums;

namespace MyLibrary.Server.Models.Result;

public class DeleteBookResult
{
    public bool Succeeded { get; set; }
    public ErrorType? Error { get; set; }
    public string Message { get; set; } = string.Empty;

    public DeleteBookResult(bool isSuccessful)
    {
        Succeeded = isSuccessful;
    }

    public static DeleteBookResult BookNotFound()
    {
        var result = new DeleteBookResult(false)
        {
            Error = ErrorType.BookNotFound,
            Message = "Book not found."
        };

        return result;
    }

    public static DeleteBookResult UserNotFound()
    {
        var result = new DeleteBookResult(false)
        {
            Error = ErrorType.UserNotFound,
            Message = "Couldn't find user."
        };
        return result;
    }

    public static DeleteBookResult Success()
    {
        var result = new DeleteBookResult(true)
        {
            Message = "Deleted book."
        };
        return result;
    }

    public static DeleteBookResult ServerError()
    {
        var result = new DeleteBookResult(false)
        {
            Error = ErrorType.Server,
            Message = "Couldn't delete book due to server error."
        };
        return result;
    }
}
