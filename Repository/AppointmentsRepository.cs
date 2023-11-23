using Scheduler_API.BHISHAK_APP_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scheduler_API.Model;
using MySql.Data.MySqlClient;
using Scheduler_API.ViewModel;
using Scheduler_API.StoreProcedures;

namespace Scheduler_API.Repository
{
    public class AppointmentsRepository : IAppointmentsRepository
    {

        private readonly bhishak_app_dbContext _context;
        private readonly StoreProceduresContext _Spcontext;
        public AppointmentsRepository(bhishak_app_dbContext context, StoreProceduresContext Spcontext)
        {
            _Spcontext = Spcontext;
            _context = context;
        }
        public async Task<int> CancelAppointment(int AppointmentId)
        {
            try
            {
                var Data = (from x in _context.TblAppointments
                            where x.PatientAppointmentId == AppointmentId
                            select x).FirstOrDefault();
                if (Data != null)
                {
                    TblAppointment obj = new TblAppointment();
                    // obj.AppointmentStatusId = 4;
                    Data.AppointmentStatusId = 4;
                    Data.AppointmentStatusId = 4;

                    var response = await _context.SaveChangesAsync();
                    return response;
                }
                return 0;
            }
            catch(Exception e)
            {
                throw e;
            }
          


        }

        private int getage(DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.Subtract(dob).Days;
            age = age / 365;
            return age;

        }
        public async Task<int> SaveAppointment(Saveresults obj)
        {
            try
            {
                TblAppointmentPatientTemp objtemp = new TblAppointmentPatientTemp();
                objtemp.FirstName = obj.FirstName;
                objtemp.LastName = obj.LastName;
                //  objtemp.Age = getage(obj.DateOfBirth);
                objtemp.Sex = obj.Gender;
                objtemp.MobileNumber = obj.MobileNumber;
                objtemp.AadharNo = obj.AadhaarNumber;
                objtemp.DateOfBirth = obj.DateOfBirth;
                objtemp.NationalityId = obj.NationalityId;
                objtemp.ReligionId = obj.RelegionId;
                objtemp.Pincode = obj.PinCode;
                objtemp.DoorNo = obj.HouseNo;
                objtemp.StateId = obj.StateId;
                objtemp.CreatedBy = obj.CretedBy;
                objtemp.CreatedDate = DateTime.Now;
                objtemp.DistrictId = obj.DistrictId;
                objtemp.City = obj.City;
                objtemp.Village = obj.Village;
                objtemp.AgeMode = obj.AgeModId;
                objtemp.Age = obj.Age;
                objtemp.Prefix = obj.Prefix;
                _context.TblAppointmentPatientTemps.Add(objtemp);
                _context.SaveChanges();

                //  var tempdata=_context.TblAppointmentPatientTemps.Where(x=>x.FirstName==obj.FirstName && )

                TblAppointment tblap = new TblAppointment();
                tblap.AppointmentDate = obj.AppointmentDate;
                tblap.ScheduleTypeId = obj.ScheduleTypeId;
                tblap.AppointmentStatus = 1;
                if (obj.SpecialityID != 0)
                {
                    tblap.SpecialityId = obj.SpecialityID;
                }
                if (obj.DoctorId != 0)
                {
                    tblap.DoctorId = obj.DoctorId;
                }
                if (obj.ChargegroupId != 0)
                {
                    tblap.ChargeGroupId = obj.ChargegroupId;
                }
                if (obj.ChargeItemId != 0)
                {
                    tblap.ChargeItemId = obj.ChargeItemId;
                }
                tblap.CreatedDate = DateTime.Now;
                tblap.OrganizationId = obj.OrganizationId;
                tblap.CreatedBy = obj.CretedBy;
                tblap.FacilityId = obj.FacilityId;
                tblap.PatientTempId = objtemp.PatientTempId;
                string stringDate = obj.AppointmentDate.ToShortDateString() + " " + obj.StartTime;
                DateTime ConvertedDate = Convert.ToDateTime(stringDate);
                tblap.StartTime =Convert.ToDateTime(obj.AppointmentDate.ToShortDateString() + " " + obj.StartTime);
                tblap.EndTime = Convert.ToDateTime(obj.AppointmentDate.ToShortDateString() +" "+obj.EndTime);
             if (obj.PatientId!=0)
                {
                    tblap.PatientId = obj.PatientId;
                }
               
                tblap.OrganizationId = obj.OrganizationId;
                tblap.CreatedBy = obj.CretedBy;

                tblap.AppointmentStatusId = 7;
                _context.TblAppointments.Add(tblap);
                var r = await _context.SaveChangesAsync();
                return r;
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }

        public async Task<int> UpdateAppointment(EditAppointmentVo obj)
        {

            try
            {
                //TblAppointment tblap = new TblAppointment();
                // TblAppointmentPatientTemp objtemp = new TblAppointmentPatientTemp();
                var Data = (from x in _context.TblAppointments
                            where x.PatientAppointmentId == obj.AppointmentId
                            select x).FirstOrDefault();

                Data.ModifiedBy = obj.ModifiedBy;
                Data.ModifiedDate = DateTime.Now;
                Data.StartTime = DateTime.Parse(obj.StartTime);
                Data.EndTime = DateTime.Parse(obj.EndTime);
                Data.SpecialityId = obj.SpecialityID;
                Data.DoctorId = obj.DoctorId;
                Data.FacilityId = obj.FacilityId;
                Data.OrganizationId = obj.OrganizationId;
                Data.AppointmentDate = obj.AppointmentDate;
                int apmnt = await _context.SaveChangesAsync();

                // TblAppointmentPatientTemp objtemp = new TblAppointmentPatientTemp();
                var objtemp = (from x in _context.TblAppointmentPatientTemps
                               where x.PatientTempId == Data.PatientTempId
                               select x).FirstOrDefault();

                objtemp.FirstName = obj.FirstName;
                if (obj.LastName != null && obj.LastName != "")
                {
                    objtemp.LastName = obj.LastName;
                }
                objtemp.AadharNo = obj.AadhaarNumber;
                objtemp.Age = obj.Age;
                objtemp.AgeMode = obj.AgeModId;
                objtemp.NationalityId = obj.NationalityId;
                objtemp.DateOfBirth = obj.DateOfBirth;
                objtemp.DistrictId = obj.DistrictId;
                objtemp.Sex = obj.Gender;
                if (obj.HouseNo != null)
                {
                    objtemp.DoorNo = obj.HouseNo;
                }
                objtemp.StateId = obj.StateId;
                objtemp.City = obj.City;
                if (obj.Village != null)
                {
                    objtemp.Village = obj.Village;
                }

                objtemp.Prefix = obj.Prefix;
                objtemp.ReligionId = obj.RelegionId;
                objtemp.Pincode = obj.PinCode;
                objtemp.ModifiedBy = obj.ModifiedBy;
                objtemp.ModifiedDate = DateTime.Now;
                int temp = _context.SaveChanges();


                if (temp == 1 && apmnt == 1)
                {
                    return temp;
                }
                else return 0;
            }
            catch(Exception e)
            {
                throw e;
            }
         
        }

        public async Task<int> RescheduleAppointment(RescheduleAppointmentVo obj)
        {

            var data = (from x in _context.TblAppointments where x.PatientAppointmentId == obj.PatientAppointmentId select x).FirstOrDefault();
                data.PatientAppointmentId = obj.PatientAppointmentId;
                  data.StartTime = DateTime.Parse(obj.StartTime);
                data.EndTime = DateTime.Parse(obj.EndTime);
            data.AppointmentDate = obj.AppointmentDate;
           return  await _context.SaveChangesAsync();

        }
        public async Task<int>TransferAppointment(TransferAppointmentVo obj)
        {
            var data = (from x in _context.TblAppointments where x.PatientAppointmentId == obj.PatientAppointmentId select x).FirstOrDefault();

           // data.PatientAppointmentId = obj.PatientAppointmentId;

            data.StartTime = DateTime.Parse(obj.StartTime);

            data.EndTime = DateTime.Parse(obj.EndTime);

            data.SpecialityId = obj.SpecialityID;

            data.AppointmentDate = obj.AppointmentDate;

            data.DoctorId = obj.DoctorId;

            return await _context.SaveChangesAsync();
        }

        //Goutham starts
        public async Task<int> SaveScheduleTemplate(List<ScheduleTemplate> obj)
        {
            try
            {
                var r = 0;
                if (obj.Count != 0)
                {
                    for (int i = 0; i < obj.Count; i++)
                    {

                        ScheduleTemplate sc = new ScheduleTemplate();
                        sc = obj[i];
                        TblProviderScheduleTemplate objtblproviderscheduletemplate = new TblProviderScheduleTemplate();
                        objtblproviderscheduletemplate.ScheduleTempalteName = sc.templateName;
                        objtblproviderscheduletemplate.ProviderId = Convert.ToInt32(sc.providerId);
                        objtblproviderscheduletemplate.FacilityId = Convert.ToInt32(sc.facilityId);
                        objtblproviderscheduletemplate.ScheduleIntravel = Convert.ToInt32(sc.interval);
                        objtblproviderscheduletemplate.ScheduleTemplateEffectiveDate = sc.effictivedate;
                        objtblproviderscheduletemplate.ScheduleTempateExpirationDate = sc.schedulevaliduptodate;
                        objtblproviderscheduletemplate.CreatedBy = sc.createdby;
                        objtblproviderscheduletemplate.ModifiedBy = sc.createdby;
                        objtblproviderscheduletemplate.CreationDate = DateTime.Now;
                        objtblproviderscheduletemplate.ModifiedDate = DateTime.Now;
                        var timespanschedulestarttime = TimeSpan.Parse(sc.schedulestarttime);
                        DateTime schedulestarttime = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date) + timespanschedulestarttime);
                        objtblproviderscheduletemplate.ScheduleStartTime = schedulestarttime;
                        var timespanscheduleendtime = TimeSpan.Parse(sc.scheduleendtime);
                        DateTime scheduleendtime = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date) + timespanscheduleendtime);
                        objtblproviderscheduletemplate.ScheduleEndTime = scheduleendtime;
                        if (i == 0)
                        {
                            _context.TblProviderScheduleTemplates.Add(objtblproviderscheduletemplate);
                            _context.SaveChanges();
                        }

                        int ScheduleTemplateId = _context.TblProviderScheduleTemplates.Max(i => i.ScheduleTemplateId);


                        TblProviderScheduleTemplatePeriod objtblproviderscheduletemplateperiod = new TblProviderScheduleTemplatePeriod();
                        objtblproviderscheduletemplateperiod.ScheduleTemplateId = ScheduleTemplateId;
                        objtblproviderscheduletemplateperiod.PeriodStart = schedulestarttime;
                        objtblproviderscheduletemplateperiod.PeriodEnd = scheduleendtime;
                        objtblproviderscheduletemplateperiod.CreatedBy = sc.createdby;
                        objtblproviderscheduletemplateperiod.ModifiedBy = sc.createdby;
                        objtblproviderscheduletemplateperiod.CreationDate = DateTime.Now;
                        objtblproviderscheduletemplateperiod.ModifiedDate = DateTime.Now;

                        if (sc.dayid == 7)
                        {
                            objtblproviderscheduletemplateperiod.IsSunday = 1;
                            objtblproviderscheduletemplateperiod.IsMonday = 1;
                            objtblproviderscheduletemplateperiod.IsTuesday = 1;
                            objtblproviderscheduletemplateperiod.IsWednesday = 1;
                            objtblproviderscheduletemplateperiod.IsThursday = 1;
                            objtblproviderscheduletemplateperiod.IsFriday = 1;
                            objtblproviderscheduletemplateperiod.IsSaturday = 1;
                        }
                        else
                        {
                            string tempdayids = sc.daysids.Substring(0, sc.daysids.Length);
                            string[] dayids = tempdayids.Split(',');

                            for (int k = 0; k < dayids.Length; k++)
                            {
                                if (dayids[k] == "0")
                                {
                                    objtblproviderscheduletemplateperiod.IsSunday = 1;
                                }
                                else if (dayids[k] == "1")
                                {
                                    objtblproviderscheduletemplateperiod.IsMonday = 1;
                                }
                                else if (dayids[k] == "2")
                                {
                                    objtblproviderscheduletemplateperiod.IsTuesday = 1;
                                }
                                else if (dayids[k] == "3")
                                {
                                    objtblproviderscheduletemplateperiod.IsWednesday = 1;
                                }
                                else if (dayids[k] == "4")
                                {
                                    objtblproviderscheduletemplateperiod.IsThursday = 1;
                                }
                                else if (dayids[k] == "5")
                                {
                                    objtblproviderscheduletemplateperiod.IsFriday = 1;
                                }
                                else if (dayids[k] == "6")
                                {
                                    objtblproviderscheduletemplateperiod.IsSaturday = 1;
                                }
                            }
                        }
                        //if (sc.dayid == 0)
                        //{
                        //    objtblproviderscheduletemplateperiod.IsSunday = 1;
                        //}
                        //else if (sc.dayid == 1)
                        //{
                        //    objtblproviderscheduletemplateperiod.IsMonday = 1;
                        //}
                        //else if (sc.dayid == 2)
                        //{
                        //    objtblproviderscheduletemplateperiod.IsTuesday = 1;
                        //}
                        //else if (sc.dayid == 3)
                        //{
                        //    objtblproviderscheduletemplateperiod.IsWednesday = 1;
                        //}
                        //else if (sc.dayid == 4)
                        //{
                        //    objtblproviderscheduletemplateperiod.IsThursday = 1;
                        //}
                        //else if (sc.dayid == 5)
                        //{
                        //    objtblproviderscheduletemplateperiod.IsFriday = 1;
                        //}
                        //else if (sc.dayid == 6)
                        //{
                        //    objtblproviderscheduletemplateperiod.IsSaturday = 1;
                        //}
                        //else
                        //{
                        //    objtblproviderscheduletemplateperiod.IsSunday = 1;
                        //    objtblproviderscheduletemplateperiod.IsMonday = 1;
                        //    objtblproviderscheduletemplateperiod.IsTuesday = 1;
                        //    objtblproviderscheduletemplateperiod.IsWednesday = 1;
                        //    objtblproviderscheduletemplateperiod.IsThursday = 1;
                        //    objtblproviderscheduletemplateperiod.IsFriday = 1;
                        //    objtblproviderscheduletemplateperiod.IsSaturday = 1;
                        //}
                        objtblproviderscheduletemplateperiod.ScheduleSlotStatusId = 1;
                        _context.TblProviderScheduleTemplatePeriods.Add(objtblproviderscheduletemplateperiod);
                        _context.SaveChanges();



                        List<ListofDates> scheduleDates = new List<ListofDates>();
                        DateTime startDate = sc.effictivedate;
                        DateTime endDate = sc.schedulevaliduptodate;
                        int Dayid = sc.dayid;

                        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                        {
                            if (Dayid == 7)
                            {
                                ListofDates lt = new ListofDates();
                                lt.date = date;
                                lt.dayofweek = Dayid.ToString();
                                //DateTime scheduleDatestartdate = Convert.ToDateTime(Convert.ToDateTime(date.Date) + timespanschedulestarttime);
                                scheduleDates.Add(lt);
                            }
                            else
                            {
                                string tempdayids = sc.daysids.Substring(0, sc.daysids.Length - 1);
                                string[] dayids = tempdayids.Split(',');
                                for (int l = 0; l < dayids.Length; l++)
                                {
                                    ListofDates lt = new ListofDates();
                                    if ((int)date.DayOfWeek == Convert.ToInt32(dayids[l]))
                                    {
                                        lt.date = date;
                                        lt.dayofweek = dayids[l];
                                        //DateTime scheduleDatestartdate = Convert.ToDateTime(Convert.ToDateTime(date.Date) + timespanschedulestarttime);
                                        scheduleDates.Add(lt);
                                    }
                                }

                                //if ((int)date.DayOfWeek == Dayid)
                                //{
                                //    //DateTime scheduleDatestartdate = Convert.ToDateTime(Convert.ToDateTime(date.Date) + timespanschedulestarttime);
                                //    scheduleDates.Add(date);
                                //}
                            }
                        }

                        long ScheduleTemlatePeriodId = _context.TblProviderScheduleTemplatePeriods.Max(i => i.ScheduleTemplatePeriodId);


                        for (int j = 0; j < scheduleDates.Count; j++)
                        {
                            TblProviderSchedule tblProviderSchedule = new TblProviderSchedule();
                            tblProviderSchedule.ProviderId = Convert.ToInt32(sc.providerId);
                            tblProviderSchedule.ScheduleTemplateId = ScheduleTemplateId;
                            tblProviderSchedule.PeriodType = 1;
                            DateTime scheduleDateperiodstart = Convert.ToDateTime(Convert.ToDateTime(scheduleDates[j].date) + timespanschedulestarttime);
                            tblProviderSchedule.PeriodStart = scheduleDateperiodstart;
                            DateTime scheduleDateperiodend = Convert.ToDateTime(Convert.ToDateTime(scheduleDates[j].date) + timespanscheduleendtime);
                            tblProviderSchedule.PeriodEnd = scheduleDateperiodend;
                            tblProviderSchedule.CreatedBy = sc.createdby;
                            tblProviderSchedule.ModifiedBy = sc.createdby;
                            tblProviderSchedule.CreationDate = DateTime.Now;
                            tblProviderSchedule.ModifiedDate = DateTime.Now;
                            tblProviderSchedule.StatusId = 1;
                            tblProviderSchedule.ScheduleTemplatePeriodId = ScheduleTemlatePeriodId;
                            tblProviderSchedule.ScheduleSlotStatusId = 1;
                            tblProviderSchedule.DayOfWeek = scheduleDates[j].dayofweek;
                            _context.TblProviderSchedules.Add(tblProviderSchedule);
                            r = await _context.SaveChangesAsync();
                        }
                    }
                }
                return r;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<ProviderScheduleTeamplateData>> GetProviderScheduleTeamplateData(int providerId)
        {
            try
            {
                //var org = _context.TblOrganizations.Where(x => x.OrganizationName == organization).FirstOrDefault();
                var _providerId = new MySqlParameter("providerId", providerId);
                var response = await _Spcontext.sp_GetProviderScheduleTeamplateData
                    .FromSqlRaw("call sp_GetProviderScheduleTeamplateData(@providerId)", _providerId).ToListAsync();


                return response;
            }
            catch(Exception e)
            {
                throw e;
            }
         
        }

        public async Task<List<ScheduleTemplatePeriodData>> GetScheduleTemplatePeriodData(int scheduleTemplateId)
        {
            //var org = _context.TblOrganizations.Where(x => x.OrganizationName == organization).FirstOrDefault();
            var _scheduleTemplateId = new MySqlParameter("ScheduleTemplateId", scheduleTemplateId);
            var response = await _Spcontext.sp_GetScheduleTemplatePeriodData
                .FromSqlRaw("call sp_GetScheduleTemplatePeriodData(@ScheduleTemplateId)", _scheduleTemplateId).ToListAsync();


            return response;
        }

        public async Task<int> RemoveScheduleTemplatePeriodDataByUsingScheduleTemplatePeriodId(int scheduleTemplatePeriodId)
        {
            int scheduleTemplateId = 0;
            int tblproviderscheduletemplatecount = 0;
            List<TblProviderSchedule> objTblProviderSchedule = new List<TblProviderSchedule>();

            objTblProviderSchedule = _context.TblProviderSchedules.Where(d => d.ScheduleTemplatePeriodId == scheduleTemplatePeriodId).ToList();
            _context.TblProviderSchedules.RemoveRange(objTblProviderSchedule);
            int temp1 = await _context.SaveChangesAsync();
            int temp2 = 0;

            if (temp1 != 0)
            {
                TblProviderScheduleTemplatePeriod objtblProviderScheduleTemplatePeriod;
                objtblProviderScheduleTemplatePeriod = _context.TblProviderScheduleTemplatePeriods.Where(d => d.ScheduleTemplatePeriodId == scheduleTemplatePeriodId).First();
                //scheduleTemplateId = from cats in abc.products
                //                     where cats.product_Name.Equals(productname)
                //                     select cats.category_Id;
                scheduleTemplateId = _context.Entry(objtblProviderScheduleTemplatePeriod).Property(u => u.ScheduleTemplateId).CurrentValue;

                _context.TblProviderScheduleTemplatePeriods.Remove(objtblProviderScheduleTemplatePeriod);
                temp2 = await _context.SaveChangesAsync();
                tblproviderscheduletemplatecount = _context.TblProviderScheduleTemplatePeriods.Count();
                if (tblproviderscheduletemplatecount == 0)
                {
                    TblProviderScheduleTemplate objTblProviderScheduleTemplate;
                    objTblProviderScheduleTemplate = _context.TblProviderScheduleTemplates.Where(d => d.ScheduleTemplateId == scheduleTemplateId).First();
                    _context.TblProviderScheduleTemplates.Remove(objTblProviderScheduleTemplate);
                    await _context.SaveChangesAsync();
                }
            }

            if (temp1 != 0 && temp2 != 0)
            {
                return temp1;
            }
            else return 0;

        }

        public async Task<int> BlockScheduleTemplatePeriod(int scheduleTemplatePeriodId)
        {
            try
            {
                var Data = (from x in _context.TblProviderScheduleTemplatePeriods
                            where x.ScheduleTemplatePeriodId == scheduleTemplatePeriodId
                            select x).FirstOrDefault();
                if (Data != null)
                {
                    TblAppointment obj = new TblAppointment();
                    // obj.AppointmentStatusId = 4;
                    Data.ScheduleSlotStatusId = 3;

                    var response = await _context.SaveChangesAsync();
                    return response;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<int> UnBlockScheduleTemplatePeriod(int scheduleTemplatePeriodId)
        {
            try
            {
                var Data = (from x in _context.TblProviderScheduleTemplatePeriods
                            where x.ScheduleTemplatePeriodId == scheduleTemplatePeriodId
                            select x).FirstOrDefault();
                if (Data != null)
                {
                    TblAppointment obj = new TblAppointment();
                    // obj.AppointmentStatusId = 4;
                    Data.ScheduleSlotStatusId = 1;

                    var response = await _context.SaveChangesAsync();
                    return response;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<List<ProviderScheduleTeamplateData>> ValidationOfEffictiveandExpiryDate(int providerId, string effectiveDate, string expirationDate)
        {
            try
            {
                var param = new MySqlParameter[] {
                        new MySqlParameter() {
                            ParameterName = "@providerId",
                            Direction = System.Data.ParameterDirection.Input,
                            Value = providerId
                        },
                         new MySqlParameter() {
                            ParameterName = "@effectiveDate",
                            Direction = System.Data.ParameterDirection.Input,
                            Value = effectiveDate
                        },
                          new MySqlParameter() {
                            ParameterName = "@expirationDate",
                            Direction = System.Data.ParameterDirection.Input,
                            Value = expirationDate
                        } };


                var response = await _Spcontext.sp_ValidationOfEffictiveandExpiryDate
                    .FromSqlRaw("call sp_ValidationOfEffictiveandExpiryDate(@providerId, @effectiveDate, @expirationDate)", param).ToListAsync();


                return response;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<int> UpdateScheduleTemplate(List<ScheduleTemplate> obj)
        {
            try
            {
                var r = 0;
                if (obj.Count != 0)
                {
                    for (int i = 0; i < obj.Count; i++)
                    {

                        ScheduleTemplate sc = new ScheduleTemplate();
                        sc = obj[i];
                       
                        var timespanschedulestarttime = TimeSpan.Parse(sc.schedulestarttime);
                        DateTime schedulestarttime = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date) + timespanschedulestarttime);
                        //objtblproviderscheduletemplate.ScheduleStartTime = schedulestarttime;
                        var timespanscheduleendtime = TimeSpan.Parse(sc.scheduleendtime);
                        DateTime scheduleendtime = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.Date) + timespanscheduleendtime);
                        string effectiveDate = schedulestarttime.ToString("yyyy-MM-dd");
                        string expirationDate = scheduleendtime.ToString("yyyy-MM-dd");
                      
                        if (sc.scheduleTemplatePeriodId == null)
                        {
                            TblProviderScheduleTemplatePeriod objtblproviderscheduletemplateperiod = new TblProviderScheduleTemplatePeriod();
                            objtblproviderscheduletemplateperiod.ScheduleTemplateId = Convert.ToInt32(sc.scheduleTemplateId);
                            objtblproviderscheduletemplateperiod.PeriodStart = schedulestarttime;
                            objtblproviderscheduletemplateperiod.PeriodEnd = scheduleendtime;
                            objtblproviderscheduletemplateperiod.CreatedBy = sc.createdby;
                            objtblproviderscheduletemplateperiod.ModifiedBy = sc.createdby;
                            objtblproviderscheduletemplateperiod.CreationDate = DateTime.Now;
                            objtblproviderscheduletemplateperiod.ModifiedDate = DateTime.Now;

                            if (sc.dayid == 7)
                            {
                                objtblproviderscheduletemplateperiod.IsSunday = 1;
                                objtblproviderscheduletemplateperiod.IsMonday = 1;
                                objtblproviderscheduletemplateperiod.IsTuesday = 1;
                                objtblproviderscheduletemplateperiod.IsWednesday = 1;
                                objtblproviderscheduletemplateperiod.IsThursday = 1;
                                objtblproviderscheduletemplateperiod.IsFriday = 1;
                                objtblproviderscheduletemplateperiod.IsSaturday = 1;
                            }
                            else
                            {
                                string tempdayids = sc.daysids.Substring(0, sc.daysids.Length);
                                string[] dayids = tempdayids.Split(',');

                                for (int k = 0; k < dayids.Length; k++)
                                {
                                    if (dayids[k] == "0")
                                    {
                                        objtblproviderscheduletemplateperiod.IsSunday = 1;
                                    }
                                    else if (dayids[k] == "1")
                                    {
                                        objtblproviderscheduletemplateperiod.IsMonday = 1;
                                    }
                                    else if (dayids[k] == "2")
                                    {
                                        objtblproviderscheduletemplateperiod.IsTuesday = 1;
                                    }
                                    else if (dayids[k] == "3")
                                    {
                                        objtblproviderscheduletemplateperiod.IsWednesday = 1;
                                    }
                                    else if (dayids[k] == "4")
                                    {
                                        objtblproviderscheduletemplateperiod.IsThursday = 1;
                                    }
                                    else if (dayids[k] == "5")
                                    {
                                        objtblproviderscheduletemplateperiod.IsFriday = 1;
                                    }
                                    else if (dayids[k] == "6")
                                    {
                                        objtblproviderscheduletemplateperiod.IsSaturday = 1;
                                    }
                                }
                            }

                            objtblproviderscheduletemplateperiod.ScheduleSlotStatusId = 1;
                            _context.TblProviderScheduleTemplatePeriods.Add(objtblproviderscheduletemplateperiod);
                            _context.SaveChanges();



                            List<ListofDates> scheduleDates = new List<ListofDates>();
                            DateTime startDate = sc.effictivedate;
                            DateTime endDate = sc.schedulevaliduptodate;
                            int Dayid = sc.dayid;

                            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                            {
                                if (Dayid == 7)
                                {
                                    ListofDates lt = new ListofDates();
                                    lt.date = date;
                                    lt.dayofweek = Dayid.ToString();
                                    //DateTime scheduleDatestartdate = Convert.ToDateTime(Convert.ToDateTime(date.Date) + timespanschedulestarttime);
                                    scheduleDates.Add(lt);
                                }
                                else
                                {
                                    string tempdayids = sc.daysids.Substring(0, sc.daysids.Length - 1);
                                    string[] dayids = tempdayids.Split(',');
                                    for (int l = 0; l < dayids.Length; l++)
                                    {
                                        ListofDates lt = new ListofDates();
                                        if ((int)date.DayOfWeek == Convert.ToInt32(dayids[l]) && date >= DateTime.Now.Date)
                                        {
                                            lt.date = date;
                                            lt.dayofweek = dayids[l];
                                            //DateTime scheduleDatestartdate = Convert.ToDateTime(Convert.ToDateTime(date.Date) + timespanschedulestarttime);
                                            scheduleDates.Add(lt);
                                        }
                                    }

                                  
                                }
                            }

                            long ScheduleTemlatePeriodId = _context.TblProviderScheduleTemplatePeriods.Max(i => i.ScheduleTemplatePeriodId);


                            for (int j = 0; j < scheduleDates.Count; j++)
                            {
                                TblProviderSchedule tblProviderSchedule = new TblProviderSchedule();
                                tblProviderSchedule.ProviderId = Convert.ToInt32(sc.providerId);
                                tblProviderSchedule.ScheduleTemplateId = Convert.ToInt32(sc.scheduleTemplateId);
                                tblProviderSchedule.PeriodType = 1;
                                DateTime scheduleDateperiodstart = Convert.ToDateTime(Convert.ToDateTime(scheduleDates[j].date) + timespanschedulestarttime);
                                tblProviderSchedule.PeriodStart = scheduleDateperiodstart;
                                DateTime scheduleDateperiodend = Convert.ToDateTime(Convert.ToDateTime(scheduleDates[j].date) + timespanscheduleendtime);
                                tblProviderSchedule.PeriodEnd = scheduleDateperiodend;
                                tblProviderSchedule.CreatedBy = sc.createdby;
                                tblProviderSchedule.ModifiedBy = sc.createdby;
                                tblProviderSchedule.CreationDate = DateTime.Now;
                                tblProviderSchedule.ModifiedDate = DateTime.Now;
                                tblProviderSchedule.StatusId = 1;
                                tblProviderSchedule.ScheduleTemplatePeriodId = ScheduleTemlatePeriodId;
                                tblProviderSchedule.ScheduleSlotStatusId = 1;
                                tblProviderSchedule.DayOfWeek = scheduleDates[j].dayofweek;
                                _context.TblProviderSchedules.Add(tblProviderSchedule);
                                r = await _context.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            int scheduleTemplatePeriodId = Convert.ToInt32(sc.scheduleTemplatePeriodId);
                            List<TblProviderSchedule> objTblProviderSchedule = null;
                            List<string> lstdays = new List<string>();
                            List<string> removelstdays = new List<string>() { "0", "1", "2", "3", "4", "5", "6" };
                            List<ListofDates> scheduleDates = new List<ListofDates>();
                            DateTime startDate = sc.effictivedate;
                            DateTime endDate = sc.schedulevaliduptodate;
                            var objtblproviderscheduletemplateperiod = (from x in _context.TblProviderScheduleTemplatePeriods
                                                                        where x.ScheduleTemplatePeriodId == scheduleTemplatePeriodId
                                                                        select x).FirstOrDefault();
                            //TblProviderScheduleTemplatePeriod objtblproviderscheduletemplateperiod = new TblProviderScheduleTemplatePeriod();
                            objtblproviderscheduletemplateperiod.ModifiedBy = sc.createdby;
                            objtblproviderscheduletemplateperiod.CreationDate = DateTime.Now;
                            objtblproviderscheduletemplateperiod.ModifiedDate = DateTime.Now;
                            objtblproviderscheduletemplateperiod.PeriodStart = schedulestarttime;
                            objtblproviderscheduletemplateperiod.PeriodEnd = scheduleendtime;
                            objtblproviderscheduletemplateperiod.IsSunday = null;
                            objtblproviderscheduletemplateperiod.IsMonday = null;
                            objtblproviderscheduletemplateperiod.IsTuesday = null;
                            objtblproviderscheduletemplateperiod.IsWednesday = null;
                            objtblproviderscheduletemplateperiod.IsThursday = null;
                            objtblproviderscheduletemplateperiod.IsFriday = null;
                            objtblproviderscheduletemplateperiod.IsSaturday = null;
                            if (sc.dayid == 7)
                            {
                                for (int j = 0; j <= 6; j++)
                                {
                                    lstdays.Add(j.ToString());
                                }
                                objtblproviderscheduletemplateperiod.IsSunday = 1;
                                objtblproviderscheduletemplateperiod.IsMonday = 1;
                                objtblproviderscheduletemplateperiod.IsTuesday = 1;
                                objtblproviderscheduletemplateperiod.IsWednesday = 1;
                                objtblproviderscheduletemplateperiod.IsThursday = 1;
                                objtblproviderscheduletemplateperiod.IsFriday = 1;
                                objtblproviderscheduletemplateperiod.IsSaturday = 1;
                            }
                            else
                            {
                                string tempdayids = sc.daysids.Substring(0, sc.daysids.Length - 1);
                                string[] dayids = tempdayids.Split(',');
                                for (int l = 0; l < dayids.Length; l++)
                                {
                                    lstdays.Add(dayids[l]);
                                    if (dayids[l] == "0")
                                    {
                                        objtblproviderscheduletemplateperiod.IsSunday = 1;
                                    }
                                    else if (dayids[l] == "1")
                                    {
                                        objtblproviderscheduletemplateperiod.IsMonday = 1;
                                    }
                                    else if (dayids[l] == "2")
                                    {
                                        objtblproviderscheduletemplateperiod.IsTuesday = 1;
                                    }
                                    else if (dayids[l] == "3")
                                    {
                                        objtblproviderscheduletemplateperiod.IsWednesday = 1;
                                    }
                                    else if (dayids[l] == "4")
                                    {
                                        objtblproviderscheduletemplateperiod.IsThursday = 1;
                                    }
                                    else if (dayids[l] == "5")
                                    {
                                        objtblproviderscheduletemplateperiod.IsFriday = 1;
                                    }
                                    else if (dayids[l] == "6")
                                    {
                                        objtblproviderscheduletemplateperiod.IsSaturday = 1;
                                    }
                                }
                            }
                            r = await _context.SaveChangesAsync();
                            for (int k = 0; k < lstdays.Count; k++)
                            {
                                objTblProviderSchedule = new List<TblProviderSchedule>();
                                objTblProviderSchedule = _context.TblProviderSchedules.Where(d => d.ScheduleTemplatePeriodId == scheduleTemplatePeriodId
                                && d.DayOfWeek.Contains(lstdays[k])).ToList();
                                if (objTblProviderSchedule != null && objTblProviderSchedule.Count > 0)
                                {
                                    for(int t=0; t< objTblProviderSchedule.Count;t++)
                                    {
                                        TblProviderSchedule obj2 = new TblProviderSchedule();
                                        obj2 = _context.TblProviderSchedules.Where(x => x.ProviderScheduleId == objTblProviderSchedule[t].ProviderScheduleId).FirstOrDefault();

                                        //objTblProviderSchedule[t].PeriodStart = schedulestarttime;
                                        //objTblProviderSchedule[t].PeriodEnd = scheduleendtime;
                                        obj2.PeriodStart = schedulestarttime;
                                        obj2.PeriodEnd = scheduleendtime;
                                        _context.SaveChanges();

                                    }
                                }
                                else
                                {
                                    for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                                    {
                                        ListofDates lt = new ListofDates();

                                        if ((int)date.DayOfWeek == Convert.ToInt32(lstdays[k]) && date >= DateTime.Now.Date)
                                        {
                                            lt.date = date;
                                            lt.dayofweek = lstdays[k];
                                            //DateTime scheduleDatestartdate = Convert.ToDateTime(Convert.ToDateTime(date.Date) + timespanschedulestarttime);
                                            scheduleDates.Add(lt);
                                        }
                                    }

                                    for (int j = 0; j < scheduleDates.Count; j++)
                                    {
                                        TblProviderSchedule tblProviderSchedule = new TblProviderSchedule();
                                        tblProviderSchedule.ProviderId = Convert.ToInt32(sc.providerId);
                                        tblProviderSchedule.ScheduleTemplateId = Convert.ToInt32(sc.scheduleTemplateId);
                                        tblProviderSchedule.PeriodType = 1;
                                        DateTime scheduleDateperiodstart = Convert.ToDateTime(Convert.ToDateTime(scheduleDates[j].date) + timespanschedulestarttime);
                                        tblProviderSchedule.PeriodStart = scheduleDateperiodstart;
                                        DateTime scheduleDateperiodend = Convert.ToDateTime(Convert.ToDateTime(scheduleDates[j].date) + timespanscheduleendtime);
                                        tblProviderSchedule.PeriodEnd = scheduleDateperiodend;
                                        tblProviderSchedule.CreatedBy = sc.createdby;
                                        tblProviderSchedule.ModifiedBy = sc.createdby;
                                        tblProviderSchedule.CreationDate = DateTime.Now;
                                        tblProviderSchedule.ModifiedDate = DateTime.Now;
                                        tblProviderSchedule.StatusId = 1;
                                        tblProviderSchedule.ScheduleTemplatePeriodId = scheduleTemplatePeriodId;
                                        tblProviderSchedule.ScheduleSlotStatusId = 1;
                                        tblProviderSchedule.DayOfWeek = scheduleDates[j].dayofweek;
                                        _context.TblProviderSchedules.Add(tblProviderSchedule);
                                        r = await _context.SaveChangesAsync();
                                    }



                                }
                            }

                            removelstdays.RemoveAll(x => lstdays.Exists(y => y == x));

                            for (int m = 0; m < removelstdays.Count; m++)
                            {
                                List<TblProviderSchedule> objTblProviderScheduleremove = new List<TblProviderSchedule>();

                                objTblProviderScheduleremove = _context.TblProviderSchedules.Where(d => d.ScheduleTemplatePeriodId == scheduleTemplatePeriodId &&
                                d.DayOfWeek.Contains(removelstdays[m])).ToList();
                                _context.TblProviderSchedules.RemoveRange(objTblProviderScheduleremove);
                                r = await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
                return r;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ListofDates>> GetProviderScheduleDates(int scheduletemplateperiodid, string fromdate = "", string todate = "")
        {
            try
            {
                var param = new MySqlParameter[] {
                        new MySqlParameter() {
                            ParameterName = "@scheduletemplateperiodid",
                            Direction = System.Data.ParameterDirection.Input,
                            Value = scheduletemplateperiodid
                        },
                         new MySqlParameter() {
                            ParameterName = "@fromdate",
                            Direction = System.Data.ParameterDirection.Input,
                            Value = fromdate
                        },
                          new MySqlParameter() {
                            ParameterName = "@todate",
                            Direction = System.Data.ParameterDirection.Input,
                            Value = todate
                        } };


                var response = await _Spcontext.sp_GetProviderScheduleDates
                    .FromSqlRaw("call sp_GetProviderScheduleDates(@scheduletemplateperiodid, @fromdate, @todate)", param).ToListAsync();


                return response;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<int> BlockScheduleTemplateDateWise(List<ListofDates> obj)
        {
            try
            {
                var r = 0;
                if (obj.Count > 0)
                {
                    for (int i = 0; i < obj.Count; i++)
                    {
                        ListofDates listofDates = new ListofDates();
                        listofDates = obj[i];
                        TblProviderSchedule objtblproviderschedules = new TblProviderSchedule();
                        objtblproviderschedules = (from x in _context.TblProviderSchedules
                                                   where x.ProviderScheduleId == Convert.ToInt32(listofDates.providerScheduleId)
                                                   select x).FirstOrDefault();
                        if (objtblproviderschedules != null)
                        {
                            objtblproviderschedules.ModifiedBy = objtblproviderschedules.CreatedBy;
                            objtblproviderschedules.ModifiedDate = DateTime.Now;
                            objtblproviderschedules.ScheduleSlotStatusId = 3;
                            r = await _context.SaveChangesAsync();
                        }

                        var Data = (from x in _context.TblProviderScheduleTemplatePeriods
                                    where x.ScheduleTemplatePeriodId == objtblproviderschedules.ScheduleTemplatePeriodId
                                    select x).FirstOrDefault();
                        if (Data != null)
                        {

                            Data.ScheduleSlotStatusId = 3;
                            r = await _context.SaveChangesAsync();
                        }
                    }

                }
                return r;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ListofDates>> GetProviderBlockedScheduleDates(int scheduletemplateperiodid, string fromdate = "", string todate = "")
        {
            try
            {
                var param = new MySqlParameter[] {
                        new MySqlParameter() {
                            ParameterName = "@scheduletemplateperiodid",
                            Direction = System.Data.ParameterDirection.Input,
                            Value = scheduletemplateperiodid
                        },
                         new MySqlParameter() {
                            ParameterName = "@fromdate",
                            Direction = System.Data.ParameterDirection.Input,
                            Value = (fromdate == null ? "" : fromdate)
                        },
                          new MySqlParameter() {
                            ParameterName = "@todate",
                            Direction = System.Data.ParameterDirection.Input,
                            Value = (todate == null ? "" : todate)
                        } };


                var response = await _Spcontext.sp_GetProviderBlockedScheduleDates
                    .FromSqlRaw("call sp_GetProviderBlockedScheduleDates(@scheduletemplateperiodid, @fromdate, @todate)", param).ToListAsync();


                return response;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<int> UnBlockScheduleTemplateDateWise(List<ListofDates> obj)
        {
            try
            {
                var r = 0;
                if (obj.Count > 0)
                {
                    for (int i = 0; i < obj.Count; i++)
                    {
                        ListofDates listofDates = new ListofDates();
                        listofDates = obj[i];
                        TblProviderSchedule objtblproviderschedules = new TblProviderSchedule();
                        objtblproviderschedules = (from x in _context.TblProviderSchedules
                                                   where x.ProviderScheduleId == Convert.ToInt32(listofDates.providerScheduleId)
                                                   select x).FirstOrDefault();
                        if (objtblproviderschedules != null)
                        {
                            objtblproviderschedules.ModifiedBy = objtblproviderschedules.CreatedBy;
                            objtblproviderschedules.ModifiedDate = DateTime.Now;
                            objtblproviderschedules.ScheduleSlotStatusId = 1;
                            r = await _context.SaveChangesAsync();
                        }

                        int chkcount = (from x in _context.TblProviderSchedules
                                        where x.ScheduleTemplatePeriodId == objtblproviderschedules.ScheduleTemplatePeriodId
                                        && x.ScheduleSlotStatusId == 3
                                        select x).Count();
                        if (chkcount == 0)
                        {
                            var Data = (from x in _context.TblProviderScheduleTemplatePeriods
                                        where x.ScheduleTemplatePeriodId == objtblproviderschedules.ScheduleTemplatePeriodId
                                        select x).FirstOrDefault();
                            if (Data != null)
                            {

                                Data.ScheduleSlotStatusId = 1;
                                r = await _context.SaveChangesAsync();
                            }
                        }
                        
                    }

                }
                return r;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> TarnsferAppointmentThrougScheduler(RescheduleAppointmentVo obj, string providerId)
        {

            var data = (from x in _context.TblAppointments where x.PatientAppointmentId == obj.PatientAppointmentId select x).FirstOrDefault();
            data.PatientAppointmentId = obj.PatientAppointmentId;
            data.StartTime = DateTime.Parse(obj.StartTime);
            data.EndTime = DateTime.Parse(obj.EndTime);
            data.AppointmentDate = DateTime.Now.Date;
            data.DoctorId = Convert.ToInt32(providerId);
            return await _context.SaveChangesAsync();
        }

        //Goutham end

    }




}
    
