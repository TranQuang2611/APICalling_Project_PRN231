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
            if(searchForm.catId != null && searchForm.catId.Count() > 0)
            {
                productList = productList.Where(x => searchForm.catId.Contains(x.CategoryId)).ToList();
            }
            if(searchForm.colorSearch != null && searchForm.colorSearch.Count() > 0)
            {
                productList = productList.Where(x => searchForm.colorSearch.Contains(x.ColorId)).ToList();
            }
            if (searchForm.sizeSearch != 0)
            {
                productList = productList.Where(x => x.SizeId == searchForm.sizeSearch).ToList();
            }
            if (searchForm.sizeSearch != 0)
            {
                productList = productList.Where(x => x.SizeId == searchForm.sizeSearch).ToList();
            }
            if (searchForm.ramSearch != 0)
            {
                productList = productList.Where(x => x.RamId == searchForm.ramSearch).ToList();
            }
            return productList;
        }
    }
}
