using MyLib.Models.Entities;

namespace MyLib.Models.RequestDto;

public class ReplyDto
{
    public string? Reply { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime PostCreationDate { get; set; }
    public int Likes { get; set;  }
}