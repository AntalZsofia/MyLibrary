using System.ComponentModel.DataAnnotations;
using MyLib.Models.Entities;
using MyLib.Models.Enums;

namespace MyLib.Models.RequestDto;

public class CreateBookDto
{
    
    public string? Title { get; set; }
    
    public string? Genre { get; set; }
    
    public string? PublishDate { get; set; }

    public string? Author { get; set; }
    public string? Description { get; set; }
    public string? SmallCoverImage { get; set; }

}