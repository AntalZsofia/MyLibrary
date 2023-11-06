using System.ComponentModel.DataAnnotations.Schema;

namespace MyLib.Models.Entities;

public class DiscussionThread
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ICollection<ForumPost> ForumPosts { get; set; }
}