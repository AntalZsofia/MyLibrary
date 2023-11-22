using MyLib.Models.Enums;

namespace MyLib.Models.RequestDto;

public class ReadingStatusDto
{
    public Guid Id { get; set; }
    public ReadingStatus ReadingStatus { get; set; }
}