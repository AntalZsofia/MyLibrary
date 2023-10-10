using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyLib.Models.ResponseDto;

public class GoogleBooksApiResponse
{
    [JsonProperty("items")]
    public List<GoogleBookItem> Items { get; set; }
}

public class GoogleBookItem
{
    [JsonProperty("volumeInfo")]
    public GoogleBookVolumeInfo VolumeInfo { get; set; }
}

public class GoogleBookVolumeInfo
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("authors")]
    public List<string> Authors { get; set; }

    [JsonProperty("publishedDate")]
    public string PublishedDate { get; set; }

    [JsonProperty("categories")]
    public List<string> Categories { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("imageLinks")]
    public GoogleBookImageLinks ImageLinks { get; set; }
}

public class GoogleBookImageLinks
{
    [JsonProperty("smallThumbnail")]
    public string SmallThumbnail { get; set; }
}
