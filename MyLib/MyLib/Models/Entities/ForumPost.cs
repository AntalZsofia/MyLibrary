using System.ComponentModel.DataAnnotations.Schema;

namespace MyLib.Models.Entities;

public class ForumPost
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    
    
    public string DiscussionThread { get; set; }
    public DateTime PostCreationDate { get; set; }
    
}