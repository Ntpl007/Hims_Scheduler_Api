using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler_API.ViewModel
{
    public class ScheduleTemplatePeriodData
    {
        public string sceheduleTemplateName { get; set; }
        public string ProviderName { get; set; }
        public string facilityName { get; set; }
        public int scheduleIntravel { get; set; }
        public DateTime scheduleTemplateEffectiveDate { get; set; }
        public DateTime scheduleTemplateExpirationDate { get; set; }
        public int scheduleTemplatePeriodId { get; set; }
        public DateTime periodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public bool isMonday { get; set; }
        public bool isTuesday { get; set; }
        public bool isWednesday { get; set; }
        public bool isThursday { get; set; }
        public bool isFriday { get; set; }
        public bool isSaturday { get; set; }
        public bool isSunday { get; set; }
        public int Scheduleslotstatusid { get; set; }
        public int scheduleTemplateId { get; set; }

    }
}
