using System.ComponentModel.DataAnnotations;
using MyLibrary.Server.Models.Entities;
using MyLibrary.Server.Models.Enums;

namespace MyLibrary.Server.Models.RequestDto;

public class CreateBookDto
{
    
    public string Title { get; set; }
    
    public Genre Genre { get; set; }
    
    public DateTime PublishDate { get; set; }

    public Author Author { get; set; }

}