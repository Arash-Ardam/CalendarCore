using CalendarApplication.LiveCalendar;
using CalendarDbContext.AppDbContext;
using CalendarDomain;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.CalendarServices.DbService
{
    public class CalendarDbService : ICalendarService
    {

        private readonly ApplicationDbContext dbContext;
        
        public CalendarDbService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task AddCalendar(string calendarName)
        {
            dbContext.Calendars.Add(Calendar.CreateByName(calendarName));
            dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddEvent(string CalendarName, DateTime dateTime, string description, bool isHoliday)
        {
            // TODO: make repository 
            var calendar = dbContext.Calendars.FirstOrDefault(x => x.Name == CalendarName);

            dbContext.Entry(calendar)
                .Collection(cal => cal.Events)
                .Query()
                .Where(x => x.Date >= dateTime.AddDays(-5) || x.Date <= dateTime.AddDays(+5))
                .ToList();

            calendar.AddEvent(dateTime, description, isHoliday);

            //await dbContext.SaveChangesAsync();
            dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public async Task<bool> IsWorkingDay(string CalendarName, DateTime dateTime)
        {
            var calendar = await dbContext.Calendars.FirstOrDefaultAsync(x => x.Name == CalendarName);

            return  calendar.IsWorkingDay(dateTime);
        }

        
    }
}
