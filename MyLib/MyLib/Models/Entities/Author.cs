using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyLib.Models.Entities;

public class Author
{
    [Required]
    public Guid Id { get; set; }

   
    public string Name { get; set; }
    

    [JsonIgnore]
    public List<Book> Books { get; set; }
    
    
}