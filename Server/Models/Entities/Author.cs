using System.ComponentModel.DataAnnotations;

namespace MyLibrary.Server.Models.Entities;

public class Author
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    
    public List<Book> Books { get; set; }
    
    
}