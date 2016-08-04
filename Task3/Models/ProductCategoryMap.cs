using System;
using System.Collections.Generic;

namespace Task3
{
    public partial class ProductCategoryMap
    {
        public long CategoryId { get; set; }
        public long ProductId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Products Product { get; set; }
    }
}
