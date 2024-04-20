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

        Task<Calendar> GetCalendarByName(string calendarName);
        Task<List<DayOfWeek>> GetWeekendsByDate(string calendarName, DateTime date);
        Task RemoveCalendarByName(string calendarName);
        Task SetWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends);
    }
}
