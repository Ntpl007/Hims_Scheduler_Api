using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler_API.Model
{
    public class Saveresults
    {

        public string AadhaarNumber { get; set; }
        public DateTime AppointmentDate { get; set; }

        public int ChargeItemId { get; set; }
        public int ChargegroupId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DistrictId { get; set; }
        public int DoctorId { get; set; }
        public string EndTime { get; set; }
        public string City { get; set; }
        public int Gender { get; set; }
        public string MobileNumber { get; set; }
        public int ScheduleTypeId { get; set; }
        public string HouseNo { get; set; }
        public int NationalityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PinCode { get; set; }
         public int SpecialityID { get; set; }
        public int RelegionId { get; set; }
        public string StartTime { get; set; }
        public int StateId { get; set; }
        public string Village { get; set; }
        public string CretedBy { get; set; }
        public int FacilityId { get; set; }
        public int OrganizationId{ get; set; }
        public decimal Age { get; set; }
        public int AgeModId { get; set; }
        public string Prefix { get; set; }
        public int PatientId { get; set; }

    }
}

