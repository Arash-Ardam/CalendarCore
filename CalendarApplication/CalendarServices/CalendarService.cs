using CalendarDbContext.AppDbContext;
using CalendarDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.CalendarServices
{
    public class CalendarService : ICalendarService
    {
        private readonly ApplicationDbContext dbContext;

        public CalendarService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }








        #region Calendar CRUD services
        public async Task AddCalendarByName(string calendarName)
        {
            var entryCalendar = CalendarDomain.Calendar.CreateByName(calendarName);

            if (!string.IsNullOrEmpty(calendarName))
            {
                await dbContext.Calendars.AddAsync(entryCalendar);
                await dbContext.SaveChangesAsync();
            }
            else
                throw new ArgumentNullException(nameof(calendarName));
        }

        public async Task<CalendarDomain.Calendar> GetCalendarByName(string calendarName)
        {

            var calendar = await dbContext.Calendars.FirstOrDefaultAsync(x => x.Name == calendarName);

            dbContext.Entry(calendar)
                .Collection(cal => cal.Events)
                .Query()
                .Where(x => x.Date >= DateTime.Now.AddDays(-5) || x.Date <= DateTime.Now.AddDays(+5))
                .ToList();

            return calendar;
        }
        #endregion


        //public Task AddCalendar(string calendarName)
        //{
        //    dbContext.Calendars.Add(Calendar.CreateByName(calendarName));
        //    dbContext.SaveChanges();
        //    return Task.CompletedTask;
        //}

        //public Task AddEvent(string CalendarName, DateTime dateTime, string description, bool isHoliday)
        //{
        //    // TODO: make repository 
        //    var calendar = dbContext.Calendars.FirstOrDefault(x => x.Name == CalendarName);

        //    dbContext.Entry(calendar)
        //        .Collection(cal => cal.Events)
        //        .Query()
        //        .Where(x => x.Date >= dateTime.AddDays(-5) || x.Date <= dateTime.AddDays(+5))
        //        .ToList();

        //    calendar.AddEvent(dateTime, description, isHoliday);

        //    //await dbContext.SaveChangesAsync();
        //    dbContext.SaveChanges();

        //    return Task.CompletedTask;
        //}
    }
}
