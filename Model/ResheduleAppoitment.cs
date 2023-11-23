using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler_API.Model
{
    public class ResheduleAppoitment
    {
        public int AppointmentId { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string AppointmentDate{ get; set; }

    }
}
