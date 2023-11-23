﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Scheduler_API.BHISHAK_APP_DB
{
    public partial class TblAdmEmployeeDesignation
    {
        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public string DesignationNotes { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TblAdmStatus Status { get; set; }
    }
}
