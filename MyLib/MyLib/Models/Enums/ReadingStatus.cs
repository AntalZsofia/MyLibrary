
using System.Text.Json.Serialization;

namespace MyLib.Models.Enums;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReadingStatus
{
    NotStarted,
    Reading,
    Finished
}