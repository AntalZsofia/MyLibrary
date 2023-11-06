using MyLib.Models.Entities;

namespace MyLib.Models.RequestDto;

public class CreatePostDto
{
    public string Content { get; set; }
    public DiscussionThread DiscussionThread { get; set; }
    public DateTime PostCreationDate { get; set; }
}