using System.Runtime.Serialization;

namespace MyLib.Models.Enums;

public enum Genre
{
    Fiction,
    Nonfiction,
    ShortStory,
    ScienceFiction,
[EnumMember(Value = "Mystery")]
    Mystery,
    Horror,
    HistoricalFiction,
    LiteraryFiction,
    GraphicNovel,
    Thriller,
    MagicalRealism,
    ChildrenLiterature,
    Poetry
}