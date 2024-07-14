using CalendarApplication.CalendarServices;
using CalendarDomain;
using CalendarRestApi.DTOs.Event;
using CalendarRestApi.IdentityConfigs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace CalendarRestApi.Controllers.Admin
{
    [Route("[controller]")]
    [ApiController]
    //[ApiVersion(VersioningTemplates.V02_00, VersioningTemplates.CalendarGroupName)]


    public class CalendarController : ControllerBase, ICalendarController
    {
        private readonly ICalendarService Service;
        private readonly ILogger<CalendarController> _logger;
        public CalendarController(ICalendarService service, ILogger<CalendarController> logger)
        {
            this.Service = service;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Get Apis

        [HttpGet("{calendarName}/HolidaysWithPeriodDate/{startDate}/{endDate}", Name = "GetHolidaysWithPeriodDate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetEventDto>))]
        public async Task<IActionResult> GetHolidaysWithPeriodDate(string calendarName, DateTime startDate, DateTime endDate)
        {

            List<GetEventDto> events = await Service.GetHolidaysWithPeriodDate(calendarName, startDate, endDate);
            return Ok(events);
        }


        [HttpGet("{calendarName}/IsWorkingDay/{date}", Name = "GetIsWorkingDay")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> GetIsWorkingDay(string calendarName, DateTime date)
        {
            var result = await Service.GetIsWorkingDay(calendarName, date);
            return Ok(result);
        }

        [HttpGet("{calendarName}/NextWorkingDate/{date}", Name = "GetNextWorkingDate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DateTime))]
        public async Task<IActionResult> GetNextWorkingDate(string calendarName, DateTime date)
        {
            DateTime nextWorkingDate = await Service.GetNextWorkingDate(calendarName, date);
            return Ok(nextWorkingDate);
        }

        [HttpGet("{calendarName}/GetNextWorkingsDate/{date}/{step}", Name = "GetNextWorkingsDate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DateTime>))]
        public async Task<IActionResult> GetNextWorkingsDate(string calendarName, DateTime date, int step)
        {
            if (step <= 0)
            {
                return Problem("step should  be Positive", statusCode: (int)HttpStatusCode.BadRequest);
            }
            List<DateTime> workingDays = await Service.GetNextWorkingsDate(calendarName, date, step);
            return Ok(workingDays);
        }

        [HttpGet("{calendarName}/StatusDate/{date}", Name = "GetStatusDate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DateTime))]
        public async Task<IActionResult> GetStatusDate(string calendarName, DateTime date)
        {
            DateTime dtatusDate = await Service.GetStatusDate(calendarName, date);
            return Ok(dtatusDate);
        }

        [HttpGet("{calendarName}/WorkingDayCount/{startDate}/{endDate}", Name = "GetWorkingDayCount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetWorkingDayCount(string calendarName, DateTime startDate, DateTime endDate)
        {
            int workingDayCount = await Service.GetWorkingDayCount(calendarName, startDate, endDate);
            return Ok(workingDayCount);
        }

        #endregion

        #region Event CRUD Apis

        //[Authorize(Roles = AuthorizationConsts.CalendarAdminRole)]
        [HttpPost("{calendarName}/Events/Add", Name = "AddEvent")]
        public async Task<IActionResult> AddEvent(string calendarName, EventDto eventDto)
        {
            await Service.AddEvent(calendarName, eventDto.Date, eventDto.Description, eventDto.IsHoliday);

            return Ok($"Event Added Succussfuly to {calendarName} Calendar");
        }

        [Authorize(Roles = AuthorizationConsts.CalendarAdminRole)]
        [HttpGet("{calendarName}/Events/{eventDate}/Get", Name = "GetEvent")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DateEvent))]
        public async Task<DateEvent> GetEvent(string calendarName, DateTime eventDate)
        {
            var existEvent = await Service.GetEvent(calendarName, eventDate);

            return existEvent;
        }

        [Authorize(Roles = AuthorizationConsts.CalendarAdminRole)]
        [HttpDelete("{calendarName}/Events/{eventDate}/Delete", Name = "RemoveEvent")]
        public async Task<IActionResult> RemoveEvent(string calendarName, DateTime eventDate, [FromQuery, BindRequired] string description)
        {
            await Service.RemoveEvent(calendarName, eventDate, description);
            return Ok("deleted");
        }

        [Authorize(Roles = AuthorizationConsts.CalendarAdminRole)]
        [HttpPut("{calendarName}/Events/Update", Name = "UpdateEvent")]
        public async Task<IActionResult> UpdateEvent(string calendarName, EventDto eventDto)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Calendar CRUD Apis

        //[Authorize(Roles = AuthorizationConsts.CalendarAdminRole)]
        [HttpPost("{calendarName}/Add", Name = "AddCalendarByName")]
        public async Task<IActionResult> AddCalendarByName(string calendarName)
        {
            await Service.AddCalendarByName(calendarName);
            return Created();
        }

        //[Authorize(Roles = AuthorizationConsts.CalendarAdminRole)]
        [HttpGet("{calendarName}/Get", Name = "GetCalendarByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Calendar))]
        public async Task<Calendar> GetCalendarByName(string calendarName)
        {
            var calendar = await Service.GetCalendarByName(calendarName);
            return calendar;
        }

        //[Authorize(Roles = AuthorizationConsts.CalendarAdminRole)]
        [HttpGet("GetAll", Name = "GetAllCalendars")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Calendar>))]
        public async Task<List<Calendar>> GetAllCalendars()
        {
            List<Calendar> calendars = await Service.GetAllCalendars();
            return calendars;
        }

       // [Authorize(Roles = AuthorizationConsts.CalendarAdminRole)]
        [HttpDelete("{calendarName}/Remove", Name = "RemoveCalendarByName")]
        public async Task<IActionResult> RemoveCalendarByName(string calendarName)
        {
            await Service.RemoveCalendarByName(calendarName);
            return Accepted("Successfully Deleted");
        }

        //[Authorize(Roles = AuthorizationConsts.CalendarAdminRole)]
        [HttpGet("{calendarName}/{date}/Weekends/Get", Name = "GetWeekendsByDate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DayOfWeek>))]
        public async Task<List<DayOfWeek>> GetWeekendsByDate(string calendarName, DateTime date)
        {
            var weekends = await Service.GetWeekendsByDate(calendarName, date);

            return weekends;
        }

        //[Authorize(Roles = AuthorizationConsts.CalendarAdminRole)]
        [HttpPut("{calendarName}/Weekends/Modify", Name = "ModifyWeekendsToCalendar")]
        public Task<IActionResult> ModifyWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends)
        {
            throw new NotImplementedException();
        }

        //[Authorize(Roles = AuthorizationConsts.CalendarAdminRole)]
        [HttpPost("{calendarName}/Weekends/Add", Name = "SetWeekendsToCalendar")]
        public async Task<IActionResult> SetWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends)
        {
            await Service.SetWeekendsToCalendar(calendarName, weekends);
            return Created();
        }

        #endregion



    }
}
