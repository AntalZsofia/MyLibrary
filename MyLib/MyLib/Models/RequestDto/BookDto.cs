using MyLib.Models.Entities;
using MyLib.Models.Enums;


namespace MyLib.Models.RequestDto;

public class BookDto
{
    public string? Title { get; set; }
    public Author? Author { get; set; }
    public DateTime PublishDate { get; set; }
    public Genre? Genre { get; set; }
    public string? Description { get; set; }
    public string? SmallCoverImage { get; set; }
    
}