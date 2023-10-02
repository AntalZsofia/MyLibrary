namespace MyLibrary.Server.Models.Result.GoogleSearchResult;

public class GoogleVolumeInfo
{
    public string? Title { get; set; }
    public List<string> Authors { get; set; }
    public string PublishedDate { get; set; }
    public List<string> Categories { get; set; }
    public string Description { get; set; }
    public GoogleImageLinks ImageLinks { get; set; }
}