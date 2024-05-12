
using CalendarRestApi.DTOs.Event;
using Microsoft.AspNetCore.Mvc;

namespace CalendarRestApi.Controllers.Admin
{
    public interface ICalendarController
    {

        #region GET Methods
        Task<IActionResult> GetIsWorkingDay(string calendarName , DateTime date);
        Task<IActionResult> GetStatusDate(string calendarName, DateTime date);
        Task<IActionResult> GetNextWorkingDate(string calendarName, DateTime date);
        Task<IActionResult> GetWorkingDayCount(string calendarName, DateTime startDate,DateTime endDate);
        Task<IActionResult> GetHolidaysWithPeriodDate(string calendarName, DateTime startDate, DateTime endDate);
        Task<IActionResult> GetNextWorkingsDate(string calendarName, DateTime date, int step);
        #endregion

        #region Event CRUD Methods
        Task<IActionResult> AddEvent(string calendarName, EventDto eventDto);
        Task<IActionResult> RemoveEvent(string calendarName, DateTime eventDate,string description);
        Task<IActionResult> UpdateEvent(string calendarName, EventDto eventDto);
        Task<IActionResult> GetEvent(string calendarName, DateTime eventDate);
        #endregion

        #region Calendar CRUD Methods
        Task<IActionResult> AddCalendarByName(string calendarName);
        Task<IActionResult> RemoveCalendarByName(string calendarName);
        Task<IActionResult> GetCalendarByName(string calendarName);

        Task<IActionResult> SetWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends);
        Task<IActionResult> ModifyWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends);
        Task<IActionResult> GetWeekendsByDate(string calendarName, DateTime Date);

        #endregion
    }
}
