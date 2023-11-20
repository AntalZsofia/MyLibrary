using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace MyLib.Models.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    [System.Text.Json.Serialization.JsonIgnore]
    public List<Book>? MyBooks { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public List<Book>? CurrentlyReading { get; set; } = new List<Book>();

    [System.Text.Json.Serialization.JsonIgnore]
    public List<Book>? FinishedReading { get; set; } = new List<Book>();
    public DateTime ProfileCreationDate { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<ForumPost>? ForumPosts { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<ForumReply>? ForumReplies { get; set; }
}