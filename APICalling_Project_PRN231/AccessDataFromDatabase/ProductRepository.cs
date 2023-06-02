using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
