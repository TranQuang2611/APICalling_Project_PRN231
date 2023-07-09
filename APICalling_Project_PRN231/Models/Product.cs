using System;
using System.Collections.Generic;

namespace APICalling_Project_PRN231.Models
{
    public partial class Product
    {
        public Product()
        {
            Reviews = new HashSet<Review>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImg { get; set; }
        public int? RamId { get; set; }
        public int? BrandId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public decimal? UnitSellPrice { get; set; }
        public int? UnitInStock { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Color? Color { get; set; }
        public virtual Ram? Ram { get; set; }
        public virtual Size? Size { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
