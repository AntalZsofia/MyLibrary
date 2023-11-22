namespace MyLib.Models.RequestDto;

public class BookReviewDto
{
    public Guid Id { get; set; }
    public string Review { get; set; }
    public int Rating { get; set; }
}