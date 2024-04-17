using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarDomain.Dtos
{
    public class GetCalendarDTO
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int CalendarTypeId { get; set; }
        public string? CalendarTypeName { get; set; }
    }
}
