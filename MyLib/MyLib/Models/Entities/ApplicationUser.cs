using Microsoft.AspNetCore.Identity;

namespace MyLib.Models.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    
    public List<Book> MyBooks { get; set; }
}