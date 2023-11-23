namespace MyLib.Models.ResponseDto;

public class BookReviewPrewDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? SmallCoverImage { get; set; }
    public string? Genre { get; set; }
    public string? Review { get; set; }
    public int? Rating { get; set; }
}