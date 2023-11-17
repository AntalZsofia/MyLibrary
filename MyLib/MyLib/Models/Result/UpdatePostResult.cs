namespace MyLib.Models.Result;

public class UpdatePostResult
{
    public bool Succeeded { get; private set; }
    
    public string Message { get; set; } = string.Empty;
    

    public static UpdatePostResult Success(string postSuccessfullyUpdated)
    {
        return new UpdatePostResult() { Succeeded = true, Message = "Post successfully updated."};
    }
    public static UpdatePostResult Fail(string postUpdateFailed)
    {
        return new UpdatePostResult() { Succeeded = false, Message = "Post with given id doesnt exists."};
    }
}