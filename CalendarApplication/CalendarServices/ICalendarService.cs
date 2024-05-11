using CalendarDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.CalendarServices
{
    public interface ICalendarService
    {
        Task AddCalendarByName(string calendarName);
        Task AddEvent(string calendarName, DateTime date, string description, bool isHoliday);
        Task<Calendar> GetCalendarByName(string calendarName);
        Task<DateEvent> GetEvent(string calendarName, DateTime eventDate);
        Task<List<GetEventDto>> GetHolidaysWithPeriodDate(string calendarName, DateTime startDate, DateTime endDate);
        Task<bool> GetIsWorkingDay(string calendarName, DateTime date);
        Task<DateTime> GetNextWorkingDate(string calendarName, DateTime date);
        Task<List<DateTime>> GetNextWorkingsDate(string calendarName, DateTime date, int step);
        Task<DateTime> GetStatusDate(string calendarName, DateTime date);
        Task<List<DayOfWeek>> GetWeekendsByDate(string calendarName, DateTime date);
        Task<int> GetWorkingDayCount(string calendarName, DateTime startDate, DateTime endDate);
        Task RemoveCalendarByName(string calendarName);
        Task RemoveEvent(string calendarName, DateTime eventDate,string description);
        Task SetWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends);
    }
}
