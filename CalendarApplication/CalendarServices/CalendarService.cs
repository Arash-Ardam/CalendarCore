﻿using CalendarDbContext.AppDbContext;
using CalendarDbContext.Repositories;
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
        private readonly ICalendarRepository calendarRepository;

        public CalendarService(ICalendarRepository calendarRepository)
        {
            this.calendarRepository = calendarRepository;
        }


        #region Event CRUD services

        public async Task AddEvent(string calendarName, DateTime date, string description, bool isHoliday)
        {
            var calendar = await calendarRepository.GetCalendarWithoutEvents(calendarName);

            calendar.AddEvent(date, description, isHoliday);

            await calendarRepository.SaveChangesAsync();
        }

        public async Task<DateEvent> GetEvent(string calendarName, DateTime eventDate)
        {
            var calendar = await calendarRepository.GetCalemderByNameAndEvents(calendarName,eventDate.AddDays(-15),eventDate.AddDays(15));
            var existEvent = calendar.Events.Find(x => x.Date == eventDate);

            if (null == existEvent)
            {
                throw new EventNotFoundException(eventDate);
            }

            return existEvent;
        }

        #endregion

        #region Calendar CRUD services
        public async Task AddCalendarByName(string calendarName)
        {
            var entryCalendar = CalendarDomain.Calendar.CreateByName(calendarName);

            await calendarRepository.AddCalendarByName(entryCalendar);

        }

        public async Task<CalendarDomain.Calendar> GetCalendarByName(string calendarName)
        {
            var calendar = await  calendarRepository.GetCalendarWithoutEvents(calendarName);

            if(null ==  calendar)
            {
                throw new CalendarNotFoundException(calendarName);
            }
            
            return calendar;
        }

        public async Task<List<DayOfWeek>> GetWeekendsByDate(string calendarName, DateTime date)
        {
            var calendar = await calendarRepository.GetCalendarWithoutEvents(calendarName);

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
            
            await calendarRepository.RemoveCalendarByName(entity);
        }

        public async Task SetWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends)
        {
            var calendar = await calendarRepository.GetCalendarWithoutEvents(calendarName);

            if (null == calendar)
                throw new CalendarNotFoundException(calendarName);

            calendar.SetDefaultWeekend(DateTime.Now, weekends);

            await calendarRepository.SetWeekendModified(calendar);
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
