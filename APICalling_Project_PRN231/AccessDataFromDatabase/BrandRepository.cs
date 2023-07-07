using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class CategoryRepository
    {
        private readonly ReviewStoreContext _context;

        public CategoryRepository(ReviewStoreContext context)
        {
            _context = context;
        }

        public List<Models.Category> GetAllCategory()
        {
            return _context.Categories.ToList();
        }
    }
}
