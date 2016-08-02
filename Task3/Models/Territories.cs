﻿using System;
using System.Collections.Generic;

namespace nnnn
{
    public partial class Territories
    {
        public Territories()
        {
            EmployeeTerritories = new HashSet<EmployeeTerritories>();
        }

        public string TerritoryId { get; set; }
        public string TerritoryDescription { get; set; }
        public long RegionId { get; set; }

        public virtual ICollection<EmployeeTerritories> EmployeeTerritories { get; set; }
        public virtual Region Region { get; set; }
    }
}
