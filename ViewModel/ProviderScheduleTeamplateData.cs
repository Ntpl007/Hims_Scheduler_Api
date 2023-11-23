using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler_API.Model
{
    public class ProviderScheduleTeamplateData
    {
        public string ScheduleTemplateId { get; set; }
        public string SceheduleTemplateName { get; set; }
        public string FacilityName { get; set; }
        public string ScheduleTemplateEffectiveDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ScheduleTemplateExpirationDate { get; set; }
        public string ScheduleIntravel { get; set; }
        public string AppointmentsPerSlots { get; set; }

    }
}
