using CalendarDbContext.AppDbContext;
using CalendarDomain;
using CalendarDomain.Exceptions;
using CalendarDomain.Exceptions.Calendar;
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

            await dbContext.Calendars.AddAsync(entryCalendar);
            await dbContext.SaveChangesAsync();

        }

        public async Task<CalendarDomain.Calendar> GetCalendarByName(string calendarName)
        {
            var calendar = await dbContext.Calendars.FirstOrDefaultAsync(x => x.Name == calendarName);

            if(null ==  calendar)
            {
                throw new CalendarNotFoundException(calendarName);
            }

            dbContext.Entry(calendar)
                .Collection(cal => cal.Events)
                .Query()
                .Where(x => x.Date >= DateTime.Now.AddDays(-5) || x.Date <= DateTime.Now.AddDays(+5))
                .ToList();

            return calendar;
        }

        public async Task<List<DayOfWeek>> GetWeekendsByDate(string calendarName, DateTime date)
        {
            var calendar = await dbContext.Calendars.FirstOrDefaultAsync(x => x.Name == calendarName);

            if (null == calendar)
            {
                throw new CalendarNotFoundException(calendarName);
            }

            var weekends = calendar.Weekend.Get(date);

            return weekends;

        }

        public async Task RemoveCalendarByName(string calendarName)
        {
            var entity = CalendarDomain.Calendar.CreateByName(calendarName);

            dbContext.Calendars.Remove(entity);
            await dbContext.SaveChangesAsync();

        }

        public Task SetWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends)
        {
            var calendar = dbContext.Calendars
                .FirstOrDefault(x => x.Name == calendarName);

            if(null == calendar)
                throw new CalendarNotFoundException(calendarName);

            calendar.SetDefaultWeekend(DateTime.Now, weekends);

            dbContext.Entry(calendar).Property(x => x.Weekend).IsModified = true;            

             dbContext.SaveChanges();
            return Task.CompletedTask;

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
