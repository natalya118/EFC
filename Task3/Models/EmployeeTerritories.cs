using System;
using System.Collections.Generic;

namespace nnnn
{
    public partial class EmployeeTerritories
    {
        public long EmployeeId { get; set; }
        public string TerritoryId { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Territories Territory { get; set; }
    }
}
