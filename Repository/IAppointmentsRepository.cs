using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scheduler_API.Model;
using Scheduler_API.ViewModel;

namespace Scheduler_API.Repository
{
    public interface IAppointmentsRepository
    {
        public Task<int> CancelAppointment(int AppointmentId);
        public Task<int> SaveAppointment(Saveresults obj);
        public Task<int> UpdateAppointment(EditAppointmentVo obj);
        public Task<int> RescheduleAppointment(RescheduleAppointmentVo obj);
        public Task<int> TransferAppointment(TransferAppointmentVo obj);
        public Task<int> SaveScheduleTemplate(List<ScheduleTemplate> obj);
        public Task<List<ProviderScheduleTeamplateData>> GetProviderScheduleTeamplateData(int providerId);
        public Task<List<ScheduleTemplatePeriodData>> GetScheduleTemplatePeriodData(int scheduleTemplateId);
        public Task<int> RemoveScheduleTemplatePeriodDataByUsingScheduleTemplatePeriodId(int scheduleTemplatePeriodId);
        public Task<int> BlockScheduleTemplatePeriod(int scheduleTemplatePeriodId);
        public Task<int> UnBlockScheduleTemplatePeriod(int scheduleTemplatePeriodId);
        public Task<List<ProviderScheduleTeamplateData>> ValidationOfEffictiveandExpiryDate(int providerId, string effectiveDate, string expirationDate);
        public Task<int> UpdateScheduleTemplate(List<ScheduleTemplate> obj);
        public Task<List<ListofDates>> GetProviderScheduleDates(int scheduletemplateperiodid, string fromdate, string todate);
        public Task<int> BlockScheduleTemplateDateWise(List<ListofDates> obj);
        public Task<List<ListofDates>> GetProviderBlockedScheduleDates(int scheduletemplateperiodid, string fromdate, string todate);
        public Task<int> UnBlockScheduleTemplateDateWise(List<ListofDates> obj);
        public Task<int> TarnsferAppointmentThrougScheduler(RescheduleAppointmentVo obj, string providerId);

    }
   
}
