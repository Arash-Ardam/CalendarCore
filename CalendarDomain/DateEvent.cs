namespace CalendarDomain;

public class DateEvent
{
    public int Id { get; internal set; }
    public DateTime Date { get; internal set; }
    public string? Description { get; internal set; }
    public bool IsHoliday { get; internal set; }
}