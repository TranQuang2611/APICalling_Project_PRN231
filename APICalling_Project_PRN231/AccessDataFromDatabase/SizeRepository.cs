using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class SizeRepository
    {
        private readonly ReviewStoreContext _context;

        public SizeRepository(ReviewStoreContext context)
        {
            _context = context;
        }

        public List<Models.Size> GetAllSize()
        {
            return _context.Sizes.ToList();
        }
    }
}
