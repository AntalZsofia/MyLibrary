using MyLib.Models.Entities;

namespace MyLib.Models.ResponseDto;

public class BookListResponseDto
{
    public List<Book> Books { get; set; }
}