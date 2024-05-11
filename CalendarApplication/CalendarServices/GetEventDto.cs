using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApplication.CalendarServices
{
    public class GetEventDto
    {
        public DateTime Date { get;  set; }
        public string? Description { get;  set; }
        public bool IsHoliday { get;  set; }
        public string calendarTypeName { get; set; }
    }
}
