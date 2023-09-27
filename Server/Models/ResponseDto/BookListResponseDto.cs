using MyLibrary.Server.Models.Entities;

namespace MyLibrary.Server.Models.ResponseDto;

public class BookListResponseDto
{
    public List<Book> Books { get; set; }
}