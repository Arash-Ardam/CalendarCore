using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class CalendarController : Controller 
    {
        private readonly MyCalendarApi api;

        public CalendarController(MyCalendarApi calendarApi)
        {
            api = calendarApi;
        }

        public async Task<IActionResult> GetAllCalendarsAsync()
        {

           var result = await api.GetAllCalendarsAsync();

            return View("GetAllCalendars", result);
        }
    }
}
