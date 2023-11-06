using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace MyLib.Models.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    [System.Text.Json.Serialization.JsonIgnore]
    public List<Book> MyBooks { get; set; }
    public DateTime ProfileCreationDate { get; set; }
    public ICollection<ForumPost> ForumPosts { get; set; }
}