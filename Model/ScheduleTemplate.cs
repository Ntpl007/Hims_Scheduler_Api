using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler_API.Model
{
    public class ScheduleTemplate
    {
       public string schedulestarttime { get; set; }
       public string scheduleendtime { get; set; }
       public int dayid { get; set; }
       public string templateName { get; set; }
       public DateTime effictivedate { get; set; }
       public DateTime schedulevaliduptodate { get; set; }
       public string providername { get; set; }
       public string facilityname { get; set; }
       public string interval { get; set; }
       public string providerId { get; set; } 
       public string facilityId { get; set; }
       public string createdby { get; set; }
       public string daysids { get; set; }
       public string scheduleTemplatePeriodId { get; set; }
       public string scheduleTemplateId { get; set; }
    }
}
