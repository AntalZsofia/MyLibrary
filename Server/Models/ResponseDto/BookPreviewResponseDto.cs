using MyLibrary.Server.Models.Entities;
using MyLibrary.Server.Models.Enums;

namespace MyLibrary.Server.Models.ResponseDto;

public class BookPreviewResponseDto
{
    public string Title { get; set; }
    
    public Genre Genre { get; set; }
    
    public DateTime PublishDate { get; set; }
    
    public Author Author { get; set; }
}