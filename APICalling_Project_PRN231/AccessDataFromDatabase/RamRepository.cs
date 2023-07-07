using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class RamRepository
    {
        private readonly ReviewStoreContext _context;

        public RamRepository(ReviewStoreContext context)
        {
            _context = context;
        }

        public List<Models.Ram> GetAllRam()
        {
            return _context.Rams.ToList();
        }
    }
}
