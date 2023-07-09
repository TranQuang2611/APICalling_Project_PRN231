using APICalling_Project_PRN231.Models;

namespace APICalling_Project_PRN231.DTO
{
    public class ProductDTO
    {
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
        public decimal AverageStar { get; set; }
        public bool? IsActive { get; set; }

        public BrandDTO? Brand { get; set; }
        public CategoryDTO? Category { get; set; }
        public ColorDTO? Color { get; set; }
        public RamDTO? Ram { get; set; }
        public SizeDTO? Size { get; set; }
        public List<ReviewDTO>? Reviews { get; set; }
    }
}
