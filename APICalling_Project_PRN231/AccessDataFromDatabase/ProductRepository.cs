using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class ProductRepository
    {
        private readonly ReviewStoreContext _context;

        public ProductRepository(ReviewStoreContext context)
        {
            _context = context;
        }

        public List<Product> GetListProduct()
        {
            List<Product> products = _context.Products.Include(x => x.Brand).Include(x => x.Category).Include(x => x.Color).Include(x => x.Ram).Include(x => x.Size).ToList();
            return products;
        }
        public List<Product> GetNewestProduct()
        {
            List<Product> products = _context.Products.Include(x => x.Brand).Include(x => x.Category).Include(x => x.Color).Include(x => x.Ram).Include(x => x.Size).OrderByDescending(x => x.ProductId).Take(6).ToList();
            return products;
        }

        public List<Product> GetFeartureProduct()
        {
            List<int?> idProd = _context.Reviews.OrderByDescending(x => x.ReviewDate).GroupBy(x => x.ProductId).Select(n => new { ProductId = n.Key, AverageRating = n.Average(x => x.Rating)})
                .OrderByDescending(n => n.AverageRating).Take(6).Select(n => n.ProductId).ToList();
            List<Product> products = _context.Products.Include(x => x.Brand).Include(x => x.Category).Include(x => x.Color).Include(x => x.Ram).Include(x => x.Size).Where(x => idProd.Contains(x.ProductId)).ToList();
            return products;
        }

        public Product GetProduct()
        {
            Product products = _context.Products.FirstOrDefault();
            return products;
        }

        public List<Product> SearchProduct(SearchForm searchForm)
        {
            List<Product> productList = _context.Products.Include(x => x.Size).Include(x => x.Ram).Include(x => x.Category).Include(x => x.Color).ToList(); 
            var listSearchSize = searchForm.sizeId.Where(x => x != null).ToList();
            var listSearchRam = searchForm.ramId.Where(x => x != null).ToList();
            if (!string.IsNullOrEmpty(searchForm.nameProd))
            {
                productList = productList.Where(x => x.ProductName.ToLower().Contains(searchForm.nameProd.ToLower().Trim())).ToList();
            }
            if (searchForm.catId != null && searchForm.catId.Count() > 0)
            {
                productList = productList.Where(x => searchForm.catId.Contains(x.CategoryId)).ToList();
            }
            if(searchForm.colorId != null && searchForm.colorId.Count() > 0)
            {
                productList = productList.Where(x => searchForm.colorId.Contains(x.ColorId)).ToList();
            }
            if (searchForm.brandId != null && searchForm.brandId.Count() >0 )
            {
                productList = productList.Where(x => searchForm.brandId.Contains(x.BrandId)).ToList();
            }
            if (listSearchSize != null && listSearchSize.Count() > 0) 
            {
                productList = productList.Where(x => listSearchSize.Contains(x.SizeId)).ToList();
            }
            if (listSearchRam != null && listSearchRam.Count() > 0)
            {
                productList = productList.Where(x => listSearchRam.Contains(x.RamId)).ToList();
            }
            if (!string.IsNullOrEmpty(searchForm.minPrice))
            {
                decimal priceFrom = Convert.ToDecimal(searchForm.minPrice);
                productList = productList.Where(x => x.UnitSellPrice >= priceFrom).ToList();
            }
            if (!string.IsNullOrEmpty(searchForm.maxPrice))
            {
                decimal priceTo = Convert.ToDecimal(searchForm.maxPrice);
                productList = productList.Where(x => x.UnitSellPrice <= priceTo).ToList();
            }
            return productList;
        }

        public Product GetProductDetail(int id)
        {
            Product product = _context.Products.Include(x => x.Category)
                .Include(x => x.Brand).Include(x => x.Reviews)
                .Include(x => x.Color).Include(x => x.Ram)
                .Include(x => x.Size).FirstOrDefault(x => x.ProductId == id);
            return product;
        }

        public void UpdateProductById(ProductDTO dto)
        {
            Product product = _context.Products.FirstOrDefault(x => x.ProductId == dto.ProductId);
            product.ProductName = dto.ProductName;
            product.ColorId = dto.ColorId;
            product.SizeId = dto.SizeId;
            product.RamId = dto.RamId;
            product.UnitSellPrice = dto.UnitSellPrice;
            product.UnitInStock = dto.UnitInStock;
            product.Description = dto.Description;
            _context.Products.Update(product);
        }

        public Product Create(ProductDTO dto)
        {
            Product product = new Product();
            product.CategoryId = dto.CategoryId;
            product.BrandId = dto.BrandId;  
            product.ProductName = dto.ProductName;
            product.ColorId = dto.ColorId;
            product.SizeId = dto.SizeId;
            product.RamId = dto.RamId;
            product.UnitPrice = dto.UnitPrice;
            product.UnitSellPrice = dto.UnitSellPrice;
            product.UnitInStock = dto.UnitInStock;
            product.Description = dto.Description;
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
    }
}
