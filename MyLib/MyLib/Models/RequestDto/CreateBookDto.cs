using System.ComponentModel.DataAnnotations;
using MyLib.Models.Entities;
using MyLib.Models.Enums;

namespace MyLib.Models.RequestDto;

public class CreateBookDto
{
    
    public string? Title { get; set; }
    
    public Genre? Genre { get; set; }
    
    public DateTime PublishDate { get; set; }

    public Author? Author { get; set; }
    public string? Description { get; set; }
    public string? SmallCoverImage { get; set; }

}