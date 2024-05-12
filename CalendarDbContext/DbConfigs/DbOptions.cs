using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarDbContext.DbConfigs
{
    public class DbOptions
    {
        public bool isEnabled { get; set; }
        public string connectionString { get; set; } = string.Empty;
        public int maxRetry { get; set; }
    }
}
