using System.ComponentModel.DataAnnotations;

namespace MyLib.Models.Entities;

public class Author
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    
    public List<Book> Books { get; set; }
    
    
}