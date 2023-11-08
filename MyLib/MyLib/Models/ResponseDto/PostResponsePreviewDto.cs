namespace MyLib.Models.ResponseDto;

public class PostResponsePreviewDto
{
   
    public string? ContentPreview { get; set; }
    public Guid UserId { get; set; }
    public string? DiscussionThread { get; set; }
    public DateTime PostCreationDate { get; set; }
    public int Likes { get; set;  }

}