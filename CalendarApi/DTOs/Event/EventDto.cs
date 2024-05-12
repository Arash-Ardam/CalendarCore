namespace CalendarRestApi.DTOs.Event
{
    public class EventDto
    {
        public DateTime Date { get;  set; }
        public string? Description { get;  set; }
        public bool IsHoliday { get;  set; }
    }
}
