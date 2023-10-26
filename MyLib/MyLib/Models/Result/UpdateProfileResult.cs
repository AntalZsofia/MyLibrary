namespace MyLib.Models.Result;

public class UpdateProfileResult
{
    public bool Succeeded { get; private set; }
    
    public string Message { get; set; } = string.Empty;
    

    public static UpdateProfileResult Success(string profileSuccessfullyUpdated)
    {
        return new UpdateProfileResult() { Succeeded = true, Message = "Profile successfully updated."};
    }
    public static UpdateProfileResult Fail(string passwordValidationFailed)
    {
        return new UpdateProfileResult() { Succeeded = false, Message = "Profile with given id doesnt exists."};
    }
}