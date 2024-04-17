using CalendarApplication;
using CalendarApplication.LiveCalendar;
using CalendarDomain;
using CalendarDomain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CalendarRestApi.Controllers.Calendar
{
    [Route("api/V2/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase

    {
        private readonly ICalendarService _calendarLiveService;

        public CalendarController(ICalendarService calendarLiveService)
        {
            _calendarLiveService = calendarLiveService;
        }


       
    }
}
