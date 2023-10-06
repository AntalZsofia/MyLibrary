using MyLib.Models.ResponseDto;

namespace MyLib.Models.Result;

public class UpdateBookResult
{
    public bool Succeeded { get; private set; }
    
    public string Message { get; set; } = string.Empty;
    public BookPreviewResponseDto? Data { get; set; }

    public static UpdateBookResult Success(BookPreviewResponseDto bookPreviewResponseDto)
    {
        return new UpdateBookResult() { Succeeded = true, Data = bookPreviewResponseDto, Message = "Book successfully updated."};
    }
    public static UpdateBookResult Fail()
    {
        return new UpdateBookResult() { Succeeded = false, Message = "Book with given id doesnt exists."};
    }
}