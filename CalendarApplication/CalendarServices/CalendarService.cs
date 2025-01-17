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


        #region GET Apis


        public async Task<List<DateTime>> GetNextWorkingsDate(string calendarName, DateTime date, int step)
        {
            // for getting (step) days after the entry date
            date = date.AddDays(1);

            var calendar = await calendarRepository.GetCalemderByNameAndEvents(calendarName,date,date.AddDays(step));
            List<DateTime> workingDays = new List<DateTime>();


            while (workingDays.Count < step) 
            {
                if (calendar.IsWorkingDay(date))
                {
                    workingDays.Add(date);
                }
                date = date.AddDays(1);
            }
            return workingDays;
        }


        public async Task<List<GetEventDto>> GetHolidaysWithPeriodDate(string calendarName, DateTime startDate, DateTime endDate)
        {
            var calendar = await calendarRepository.GetCalemderByNameAndEvents(calendarName, startDate, endDate);

            List<GetEventDto> events = new List<GetEventDto>();
            while (startDate <= endDate)
            {
                if (calendar.IsHoliday(startDate))
                {
                    var existEvent = calendar.Events.FirstOrDefault(e => e.Date == startDate);

                    var defaultEvent = new GetEventDto();
                    if (existEvent != null)
                    {
                        defaultEvent.Date = existEvent.Date;
                        defaultEvent.Description = existEvent.Description;
                        defaultEvent.IsHoliday = existEvent.IsHoliday;
                        defaultEvent.calendarTypeName = calendar.Name;

                    }
                    else
                    {
                        defaultEvent.Date = startDate;
                        defaultEvent.Description = "آخر هفته";
                        defaultEvent.IsHoliday = true;
                        defaultEvent.calendarTypeName = calendar.Name;

                    }
                    events.Add(defaultEvent);
                }
                startDate = startDate.AddDays(1);

            }

            return events;
            
        }

        public async Task<bool> GetIsWorkingDay(string calendarName, DateTime date)
        {
            var calendar = await calendarRepository.GetCalemderByNameAndEvents(calendarName, date.AddDays(-15), date.AddDays(15));

            return calendar.IsWorkingDay(date);
        }

        public async Task<DateTime> GetStatusDate(string calendarName, DateTime date)
        {
            var calendar = await calendarRepository.GetCalemderByNameAndEvents(calendarName,date.AddDays(-15), date.AddDays(15));

            while (calendar.IsHoliday(date))
            {
                date = date.AddDays(-1);
            };

            return date;
        }

        public async Task<DateTime> GetNextWorkingDate(string calendarName, DateTime date)
        {
            var calendar = await calendarRepository.GetCalemderByNameAndEvents(calendarName, date.AddDays(-15), date.AddDays(15));
            date = date.AddDays(+1);

            while (calendar.IsHoliday(date))
            {
                date = date.AddDays(+1);
            }

            return date;
        }

        public async Task<int> GetWorkingDayCount(string calendarName, DateTime startDate, DateTime endDate)
        {
            var calendar = await calendarRepository.GetCalemderByNameAndEvents(calendarName, startDate, endDate);
            int workingDays = 0;

            while (startDate <= endDate)
            {
                if (calendar.IsWorkingDay(startDate))
                {
                    ++workingDays;
                }
                    
                startDate = startDate.AddDays(1);
            }

            return workingDays;
        }

        #endregion

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

        public async Task<List<DateEvent>> GetEvents(string calendarName, DateTime startDate, DateTime endDate)
        {
            var calendar = await calendarRepository.GetLimitedCalenderByNameAndEvents(calendarName, startDate, endDate);
            var events = calendar.Events;
            return events;
        }


        public async Task RemoveEvent(string calendarName, DateTime eventDate,string description)
        {
            var calendar = await calendarRepository.GetCalemderByNameAndEvents(calendarName,eventDate.AddDays(-15),eventDate.AddDays(15));

            calendar.RemoveEvent(eventDate,description);
            await calendarRepository.SaveChangesAsync();            
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

        public async Task<List<CalendarDomain.Calendar>> GetAllCalendars()
        {
            List<CalendarDomain.Calendar> calendars = await calendarRepository.GetAllCalendarsWithoutEvents();

            return calendars;
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
