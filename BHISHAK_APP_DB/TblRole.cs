using System;
using System.Collections.Generic;

#nullable disable

namespace Scheduler_API.BHISHAK_APP_DB
{
    public partial class TblRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
