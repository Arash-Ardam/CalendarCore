using System.Text.Json.Serialization;

namespace CalendarDomain;

public class DateEvent
{
    public DateTime Date { get; internal set; }
    public string? Description { get; internal set; }
    public bool IsHoliday { get; internal set; }

    [JsonIgnore]
    public Calendar calendar { get; }
}