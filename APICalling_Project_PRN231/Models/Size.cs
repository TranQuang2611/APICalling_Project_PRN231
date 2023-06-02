using System;
using System.Collections.Generic;

namespace APICalling_Project_PRN231.Models
{
    public partial class Size
    {
        public Size()
        {
            Products = new HashSet<Product>();
        }

        public int SizeId { get; set; }
        public decimal? Size1 { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
