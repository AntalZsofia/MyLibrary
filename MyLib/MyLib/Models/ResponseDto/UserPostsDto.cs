using MyLib.Models.Entities;

namespace MyLib.Models.ResponseDto;

public class UserPostsDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public string? DiscussionThread { get; set; }
    public DateTime PostCreationDate { get; set; }
    public ApplicationUser User { get; set; }
    public int Likes { get; set; }
}