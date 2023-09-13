using MyLibrary.Server.Models.Entities;
using MyLibrary.Server.Models.Enums;

namespace MyLibrary.Server.Models.RequestDto;

public class UpdateBookDto
{
    public string Title { get; set; } = string.Empty;

    public Genre Genre { get; set; }
    
    public DateTime PublishDate { get; set; }

    public Author Author { get; set; }

}