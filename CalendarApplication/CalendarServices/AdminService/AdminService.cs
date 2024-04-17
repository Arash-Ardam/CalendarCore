using CalendarDbContext.AppDbContext;
using CalendarDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.CalendarServices.AdminService
{
    public class AdminService :IAdminService
    {
        private readonly ApplicationDbContext dbContext;

        public AdminService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddCalendarByName(string CalendarName)
        {
            Calendar calendar = Calendar.CreateByName(CalendarName);

            await dbContext.Calendars.AddAsync(calendar);
            await dbContext.SaveChangesAsync();
        }

        public Task AddEvent(string CalendarName, DateTime dateTime, string description, bool isHoliday)
        {
            throw new NotImplementedException();
        }

        public async Task AddWeekend(string CalendarName,DayOfWeek weekend)
        {
            Calendar calendar = dbContext.Calendars.First(x => x.Name == CalendarName);
        }

        public async Task<List<string>> GetCalendars()
        {
            var calendar = await dbContext.Calendars.Select(x => x.Name).ToListAsync();
            return  calendar;
        }

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
