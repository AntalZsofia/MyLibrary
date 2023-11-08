using System.ComponentModel.DataAnnotations.Schema;

namespace MyLib.Models.Entities;

public class ForumReply
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Reply { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public Guid PostId { get; set; }
    public DateTime ReplyCreationDate { get; set; }
    public int Likes { get; set;  }
}