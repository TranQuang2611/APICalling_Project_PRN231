using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class BrandRepository
    {
        private readonly ReviewStoreContext _context;

        public BrandRepository(ReviewStoreContext context)
        {
            _context = context;
        }

        public List<Brand> GetAllBrand()
        {
            return _context.Brands.ToList();
        }
    }
}
