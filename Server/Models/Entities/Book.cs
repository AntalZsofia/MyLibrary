using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyLibrary.Server.Models.Enums;

namespace MyLibrary.Server.Models.Entities;

public class Book
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    public string? Title { get; set; }
    
    [Required]
    public Genre Genre { get; set; }
    
    [Required]
    public DateTime PublishDate { get; set; }
    
    public Guid AuthorId { get; set; }
    
    [Required]
    public Author? Author { get; set; }
    
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    
}