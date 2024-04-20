using CalendarApplication.CalendarServices;
using CalendarRestApi.DTOs.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ICalendarService adminService;

        public CalendarController(ICalendarService adminService)
        {
            this.adminService = adminService;
        }

        #region Get Apis

        [HttpGet("{calendarName}/HolidaysWithPeriodDate/{startDate}/{endDate}")]
        public Task<IActionResult> GetHolidaysWithPeriodDate(string calendarName, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{calendarName}/IsWorkingDay/{date}")]
        public Task<IActionResult> GetIsWorkingDay(string calendarName, DateTime date)
        {
            throw new NotImplementedException();
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
        public Task<IActionResult> AddEvent(string calendarName, EventDto eventDto)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{calendarName}/Events/{eventDate}/Get")]
        public Task<IActionResult> GetEvents(string calendarName, DateTime eventDate)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{calendarName}/Events/{eventDate}/Delete")]
        public Task<IActionResult> RemoveEvent(string calendarName, DateTime eventDate)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{calendarName}/Events/Update")]
        public Task<IActionResult> UpdateEvent(string calendarName, EventDto eventDto)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Calendar CRUD Apis
        [HttpPost("{calendarName}/Add")]
        public async Task<IActionResult> AddCalendarByName(string calendarName)
        {
            try
            {
                await adminService.AddCalendarByName(calendarName);
                return Created();
            }
            catch (ArgumentNullException)
            {

                return Problem("calendarName Cant Be Null or Empty",statusCode: (int)HttpStatusCode.BadRequest);
            }
            catch(DbUpdateException ex) 
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException.Number == 2627)
                {
                    return Problem($"Calendar with name ({calendarName}) is already Exist", statusCode: (int)HttpStatusCode.BadRequest);
                }
                else
                {
                    return Problem("Other errors");
                }
            }
        }

        [HttpGet("{calendarName}/Get")]
        public async Task<IActionResult> GetCalendarByName(string calendarName)
        {
            try
            {
                var calendar = await adminService.GetCalendarByName(calendarName);
                return Ok(calendar);

            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException)
                    return Problem($"There is no calendar with name : {calendarName}", statusCode: (int)HttpStatusCode.NotFound);
                else
                    return Problem("Other Error");
            }
        }

        [HttpDelete("{calendarName}/Remove")]
        public Task<IActionResult> RemoveCalendarByName(string calendarName)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{calendarName}/{date}/Weekends/Get")]
        public Task<IActionResult> GetWeekendsByDate(string calendarName, DateTime Date)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{calendarName}/Weekends/Modify")]
        public Task<IActionResult> ModifyWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{calendarName}/Weekends/Add")]
        public Task<IActionResult> SetWeekendsToCalendar(string calendarName, List<DayOfWeek> weekends)
        {
            throw new NotImplementedException();
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
