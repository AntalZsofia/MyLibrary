using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MyLibrary.Server.Models.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    
    public List<Book> MyBooks { get; set; }
}