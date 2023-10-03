using MyLibrary.Server.Models.Entities;
using MyLibrary.Server.Models.Enums;
using MyLibrary.Server.Models.Result.GoogleSearchResult;

namespace MyLibrary.Server.Models.RequestDto;

public class BookDto
{
    public string? Title { get; set; }
    public Author? Author { get; set; }
    public DateTime PublishDate { get; set; }
    public Genre Genre { get; set; }
    public string? Description { get; set; }
    public GoogleImageLinks? ImageLinks { get; set; }
    
}