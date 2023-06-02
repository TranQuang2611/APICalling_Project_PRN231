using System;
using System.Collections.Generic;

namespace APICalling_Project_PRN231.Models
{
    public partial class Ram
    {
        public Ram()
        {
            Products = new HashSet<Product>();
        }

        public int RamId { get; set; }
        public int? RamSize { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
