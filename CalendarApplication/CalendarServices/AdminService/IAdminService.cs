using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.CalendarServices.AdminService
{
    public interface IAdminService
    {
        Task AddEvent(string CalendarName, DateTime dateTime, string description, bool isHoliday);

        Task AddCalendarByName(string CalendarName);
        Task AddWeekend(string CalendarName,DayOfWeek weekend);
        Task<List<string>> GetCalendars();
    }
}
