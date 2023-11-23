using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduler_API.Model;
using Scheduler_API.Repository;
using Scheduler_API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsRepository _repository;
        public AppointmentsController(IAppointmentsRepository repository)
        {
            _repository = repository;
        }
        [EnableCors("AllowAllHeaders")]
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<int> CancelAppointment(string AppointmentId)
        {
            byte[] data = Convert.FromBase64String(AppointmentId);
            string decodedString = System.Text.Encoding.UTF8.GetString(data);

            var response=await  _repository.CancelAppointment(Convert.ToInt32(decodedString));
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<int> SaveAppointment(Saveresults obj)
        {
            var response = await _repository.SaveAppointment(obj);
            return response;
        }
        [EnableCors("AllowAllHeaders")]
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<int> UpdateAppointment(EditAppointmentVo obj)
        {
            var response = await _repository.UpdateAppointment(obj);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<int> RescheduleAppointment(RescheduleAppointmentVo obj)
        {
            var response = await _repository.RescheduleAppointment(obj);
            return response;
        }
        [EnableCors("AllowAllHeaders")]
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<int> TransferAppointment(TransferAppointmentVo obj)
        {
            var response = await _repository.TransferAppointment(obj);
            return response;
        }

        //Goutham start

        [EnableCors("AllowAllHeaders")]
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<int> SaveScheduleTemplate(List<ScheduleTemplate> obj)
        {
            var response = await _repository.SaveScheduleTemplate(obj);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpGet]
        [Route("[action]")]
       // [Authorize]
        public async Task<List<ProviderScheduleTeamplateData>> GetProviderScheduleTeamplateData(int providerId)
        {
           
            var response = await _repository.GetProviderScheduleTeamplateData(providerId);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<List<ScheduleTemplatePeriodData>> GetScheduleTemplatePeriodData(int scheduleTemplateId)
        {
            var response = await _repository.GetScheduleTemplatePeriodData(scheduleTemplateId);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<int> RemoveScheduleTemplatePeriodDataByUsingScheduleTemplatePeriodId(int scheduleTemplatePeriodId)
        {
            var response = await _repository.RemoveScheduleTemplatePeriodDataByUsingScheduleTemplatePeriodId(scheduleTemplatePeriodId);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<int> BlockScheduleTemplatePeriod(int scheduleTemplatePeriodId)
        {

            var response = await _repository.BlockScheduleTemplatePeriod(scheduleTemplatePeriodId);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<int> UnBlockScheduleTemplatePeriod(int scheduleTemplatePeriodId)
        {

            var response = await _repository.UnBlockScheduleTemplatePeriod(scheduleTemplatePeriodId);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<List<ProviderScheduleTeamplateData>> ValidationOfEffictiveandExpiryDate(int providerId, string effectiveDate, string expirationDate)
        {
            var response = await _repository.ValidationOfEffictiveandExpiryDate(providerId, effectiveDate, expirationDate);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<int> UpdateScheduleTemplate(List<ScheduleTemplate> obj)
        {
            var response = await _repository.UpdateScheduleTemplate(obj);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<List<ListofDates>> GetProviderScheduleDates(int scheduletemplateperiodid, string fromdate, string todate)
        {
            var response = await _repository.GetProviderScheduleDates(scheduletemplateperiodid, fromdate, todate);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<int> BlockScheduleTemplateDateWise(List<ListofDates> obj)
        {
            var response = await _repository.BlockScheduleTemplateDateWise(obj);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<List<ListofDates>> GetProviderBlockedScheduleDates(int scheduletemplateperiodid, string fromdate, string todate)
        {
            var response = await _repository.GetProviderBlockedScheduleDates(scheduletemplateperiodid, fromdate, todate);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<int> UnBlockScheduleTemplateDateWise(List<ListofDates> obj)
        {
            var response = await _repository.UnBlockScheduleTemplateDateWise(obj);
            return response;
        }

        [EnableCors("AllowAllHeaders")]
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<int> TarnsferAppointmentThrougScheduler(RescheduleAppointmentVo obj, string providerId)
        {
            var response = await _repository.TarnsferAppointmentThrougScheduler(obj, providerId);
            return response;
        }

        //Goutham end
    }
}
