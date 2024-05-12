using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarDomain.Dtos
{
    public class AddEventDTO
    {
        public DateTime Date { get; set; }
        public bool IsHoliday { get; set; }
        public string Description { get; set; }
    }
}
