using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler_API.Model
{
    public class ListofDates
    {
        public DateTime date { get; set; }
        public string dayofweek { get; set; }
        public string blockdate { get; set; }
        public string providerScheduleId { get; set; }
    }
}
