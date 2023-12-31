﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Scheduler_API.BHISHAK_APP_DB
{
    public partial class TblAdmDepartment
    {
        public int DepartmentId { get; set; }
        public string DepartmentDescription { get; set; }
        public string Notes { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual TblAdmStatus Status { get; set; }
    }
}
