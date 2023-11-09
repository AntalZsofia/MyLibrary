namespace MyLib.Models.ResponseDto;

public class PostResponsePreviewDto
{
   public Guid Id { get; set; }
    public string? ContentPreview { get; set; }
    public string? Username { get; set; }
    
    public string? DiscussionThread { get; set; }
    public DateTime PostCreationDate { get; set; }
    public int Likes { get; set;  }

}