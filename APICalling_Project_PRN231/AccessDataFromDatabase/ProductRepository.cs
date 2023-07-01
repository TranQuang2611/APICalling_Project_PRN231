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

        public static Product GetProduct()
        {
            Product products = _context.Products.FirstOrDefault();
            return products;
        }

        public static List<Product> SearchProduct(SearchForm searchForm)
        {
            List<Product> productList = _context.Products.ToList(); 
            var listSearchSize = searchForm.sizeId.Where(x => x != null).ToList();
            var listSearchRam = searchForm.ramId.Where(x => x != null).ToList();
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
                productList = productList.Where(x => x.UnitSellPrice >= Convert.ToDecimal(searchForm.minPrice)).ToList();    
            }
            if (!string.IsNullOrEmpty(searchForm.maxPrice)){
                productList = productList.Where(x => x.UnitSellPrice <= Convert.ToDecimal(searchForm.maxPrice)).ToList();
            }
            return productList;
        }
    }
}
