using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler_API.Model
{
    public class TransferAppointmentVo
    {
        public int PatientAppointmentId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int SpecialityID { get; set; }
         public int DoctorId { get; set; }




    }
}
