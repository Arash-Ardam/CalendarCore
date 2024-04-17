using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarDomain.Dtos
{
    public class DeleteCalendarDTO
    {
        public DateTime Date { get; set; }
        public int CalendarTypeId { get; set; }
    }
}
