using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class ProductRepository
    {
        private static readonly ReviewStoreContext _context = new ReviewStoreContext();

        public static List<Product> GetListProduct()
        {
            List<Product> products = _context.Products.Include(x => x.Brand).Include(x => x.Category).Include(x => x.Color).Include(x => x.Ram).Include(x => x.Size).ToList();
            return products;
        }
        public static List<Product> GetNewestProduct()
        {
            List<Product> products = _context.Products.Include(x => x.Brand).Include(x => x.Category).Include(x => x.Color).Include(x => x.Ram).Include(x => x.Size).OrderByDescending(x => x.ProductId).Take(6).ToList();
            return products;
        }

        public static List<Product> GetFeartureProduct()
        {
            List<int?> idProd = _context.Reviews.OrderByDescending(x => x.ReviewDate).GroupBy(x => x.ProductId).Select(n => new { ProductId = n.Key, AverageRating = n.Average(x => x.Rating)})
                .OrderByDescending(n => n.AverageRating).Take(6).Select(n => n.ProductId).ToList();
            List<Product> products = _context.Products.Include(x => x.Brand).Include(x => x.Category).Include(x => x.Color).Include(x => x.Ram).Include(x => x.Size).Where(x => idProd.Contains(x.ProductId)).ToList();
            return products;
        }

        public static Product GetProduct()
        {
            Product products = _context.Products.FirstOrDefault();
            return products;
        }

        public static List<Product> SearchProduct(SearchForm searchForm)
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

        public static Product GetProductDetail(int id)
        {
            Product product = _context.Products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.Reviews).FirstOrDefault(x => x.ProductId == id);
            return product;
        }
    }
}
