using MyLib.Models.Result.GoogleSearchResult;

namespace MyLib.Models.Result;

public class BookSearchResultDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? PublishedDate { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public string? SmallCoverImage { get; set; } 
}