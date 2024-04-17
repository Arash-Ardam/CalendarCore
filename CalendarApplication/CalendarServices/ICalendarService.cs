using CalendarDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.LiveCalendar
{
    public interface ICalendarService
    {
         Task<bool> IsWorkingDay(string CalendarName, DateTime dateTime);
        Task AddEvent(string CalendarName, DateTime dateTime,string description,bool isHoliday);

        Task AddCalendar(string CalendarName);
    }
}
