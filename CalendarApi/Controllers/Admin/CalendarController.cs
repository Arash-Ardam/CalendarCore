using CalendarApplication.CalendarServices;
using CalendarRestApi.DTOs.Event;
using CalendarRestApi.ExceptionHandling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Presentation.Dtos.Admin;
using System.Net;

namespace CalendarRestApi.Controllers.Admin
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "V2")]

    public class CalendarController : ControllerBase, ICalendarController 
    {
        private readonly ICalendarService Service;

        public CalendarController(ICalendarService service)
        {
            this.Service = service;
        }

        #region Get Apis

        [HttpGet("{calendarName}/HolidaysWithPeriodDate/{startDate}/{endDate}")]
        public Task<IActionResult> GetHolidaysWithPeriodDate(string calendarName, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{calendarName}/IsWorkingDay/{date}")]
        public async Task<IActionResult> GetIsWorkingDay(string calendarName, DateTime date)
        {
           var result = await Service.GetIsWorkingDay(calendarName,date);
            return Ok(result);
        }

        [HttpGet("{calendarName}/NextWorkingDate/{date}")]
        public Task<IActionResult> GetNextWorkingDate(string calendarName, DateTime date)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{calendarName}/GetNextWorkingsDate/{date}/{step}")]
        public Task<IActionResult> GetNextWorkingsDate(string calendarName, DateTime date, int step)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{calendarName}/StatusDate/{date}")]
        public Task<IActionResult> GetStatusDate(string calendarName, DateTime date)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{calendarName}/WorkingDayCount/{startDate}/{endDate}")]
        public Task<IActionResult> GetWorkingDayCount(string calendarName, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Event CRUD Apis

        [HttpPost("{calendarName}/Events/Add")]
        public async Task<IActionResult> AddEvent(string calendarName, EventDto eventDto)
        {
            await Service.AddEvent(calendarName, eventDto.Date,eventDto.Description,eventDto.IsHoliday);

            return Ok($"Event Added Succussfuly to {calendarName} Calendar");
        }

        [HttpGet("{calendarName}/Events/{eventDate}/Get")]
        public async Task<IActionResult> GetEvent(string calendarName, DateTime eventDate)
        {
            var existEvent = await Service.GetEvent(calendarName, eventDate);

            return Ok(existEvent);
        }

        [HttpDelete("{calendarName}/Events/{eventDate}/Delete")]
        public async Task<IActionResult> RemoveEvent(string calendarName, DateTime eventDate,[FromQuery,BindRequired] string description)
        {
            await Service.RemoveEvent(calendarName, eventDate,description);
            return Ok("deleted");
        }

        [HttpPut("{calendarName}/Events/Update")]
        public async Task<IActionResult> UpdateEvent(string calendarName, EventDto eventDto)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Calendar CRUD Apis

        [HttpPost("{calendarName}/Add")]
        public async Task<IActionResult> AddCalendarByName(string calendarName)
        {
            await Service.AddCalendarByName(calendarName);
            return Created();
        }

        [HttpGet("{calendarName}/Get")]
        public async Task<IActionResult> GetCalendarByName(string calendarName)
        {
            var calendar = await Service.GetCalendarByName(calendarName);
            return Ok(calendar);
        }

        [HttpDelete("{calendarName}/Remove")]
        public async Task<IActionResult> RemoveCalendarByName(string calendarName)
        {
            await Service.RemoveCalendarByName(calendarName);
            return Accepted("Successfully Deleted");
        }

        [HttpGet("{calendarName}/{date}/Weekends/Get")]
        public async Task<IActionResult> GetWeekendsByDate(string calendarName, DateTime date)
        {
            var weekends = await Service.GetWeekendsByDate(calendarName, date);

            return Ok(weekends);
        }

        [HttpPut("{calendarName}/Weekends/Modify")]
        public Task<IActionResult> ModifyWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{calendarName}/Weekends/Add")]
        public async Task<IActionResult> SetWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends)
        {
            await Service.SetWeekendsToCalendar(calendarName, weekends);
            return Created();
        }

        #endregion


        //[HttpPost("AddCalendarByName")]
        //public async Task<IActionResult> AddCalendarByName(string name)
        //{
        //    try
        //    {
        //        await adminService.AddCalendarByName(name);

        //        return Ok("Created Successfully !!!");

        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        var sqlException = ex.InnerException as SqlException;
        //        if (sqlException.Number == 2627)
        //        {
        //            return Problem($"Calendar with name ({name}) is already Exist", statusCode: (int)HttpStatusCode.BadRequest);
        //        }
        //        else
        //        {
        //            return Problem("Other errors");
        //        }
        //    }
        //}


        //[HttpPost("AddWeekend")]
        //public async Task<IActionResult> AddWeekend(string CalendarName, DayOfWeek weekend)
        //{
        //    try
        //    {
        //        await adminService.AddWeekend(CalendarName, weekend);

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


    }
}
