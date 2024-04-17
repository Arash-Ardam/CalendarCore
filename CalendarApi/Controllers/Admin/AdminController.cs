using CalendarApplication.CalendarServices.AdminService;
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
    [Route("api/V2/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpPost("AddCalendarByName")]
        public async Task<IActionResult> AddCalendarByName(string name)
        {
            try
            {
                await adminService.AddCalendarByName(name);

                return Ok("Created Successfully !!!");

            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;
                if (sqlException.Number == 2627)
                {
                    return Problem($"Calendar with name ({name}) is already Exist", statusCode: (int)HttpStatusCode.BadRequest);
                }
                else
                {
                    return Problem("Other errors");
                }
            }
        }

        [HttpGet]
        public async Task<List<string>> GetCalendars()
        {
            return await adminService.GetCalendars();
        }

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
