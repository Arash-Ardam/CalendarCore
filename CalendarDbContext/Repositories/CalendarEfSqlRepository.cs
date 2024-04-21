using CalendarDbContext.AppDbContext;
using CalendarDomain;
using Microsoft.EntityFrameworkCore;


namespace CalendarDbContext.Repositories
{
    public class CalendarEfSqlRepository : ICalendarRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CalendarEfSqlRepository(ApplicationDbContext dbContext)
        {
                this.dbContext = dbContext;
        }

        public async Task<Calendar> GetCalendarWithoutEvents(string calendarName)
        {
            var calendar = await dbContext.Calendars.FirstOrDefaultAsync(x => x.Name == calendarName);

            return calendar;
        }

        public async Task<Calendar> GetCalemderByNameAndEvents(string calendarName, DateTime from, DateTime to)
        {
            // , DateTime.Now.AddDays(-5), DateTime.Now.AddDays(+5)

            var calendar = await dbContext.Calendars.FirstOrDefaultAsync(x => x.Name == calendarName);
            
            dbContext.Entry(calendar)
                        .Collection(cal => cal.Events)
                        .Query()
                        .Where(x => x.Date >= from || x.Date <= to)
                        .ToList();

            return calendar;
        }

        public async Task AddCalendarByName(Calendar newCalendar)
        {
            await dbContext.Calendars.AddAsync(newCalendar);
            await SaveChangesAsync();
        }

        public async Task RemoveCalendarByName(Calendar entity)
        {
            dbContext.Calendars.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task SetWeekendModified(Calendar calendar)
        {
            dbContext.Entry(calendar).Property(x => x.Weekend).IsModified = true;
            await SaveChangesAsync();
        }

        






        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

       
    }
}
