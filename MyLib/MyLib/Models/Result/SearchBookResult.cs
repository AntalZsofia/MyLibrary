namespace MyLib.Models.Result;

public class SearchBookResult
{
    public bool Succeeded { get; private set; }
    
    public string Message { get; set; } = string.Empty;
    public IEnumerable<BookSearchResultDto>? Data { get; set; }

    public static SearchBookResult Success(IEnumerable<BookSearchResultDto> bookSearchResultDtos)
    {
        return new SearchBookResult() { Succeeded = true, Data = bookSearchResultDtos, Message = "Books successfully retrieved." };
    }

    public static SearchBookResult Fail()
    {
        return new SearchBookResult() { Succeeded = false, Message = "No books found." };
    }
}