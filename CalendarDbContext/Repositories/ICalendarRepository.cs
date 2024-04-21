using CalendarDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarDbContext.Repositories
{
    public interface ICalendarRepository
    {
        Task<Calendar> GetCalemderByNameAndEvents(string calendarName, DateTime from, DateTime to);
        Task<Calendar> GetCalendarWithoutEvents(string calendarName);
        Task RemoveCalendarByName(Calendar entity);
        Task SetWeekendModified(Calendar calendar);


        Task AddCalendarByName(Calendar newCalendar);

        Task SaveChangesAsync();
    }
}
