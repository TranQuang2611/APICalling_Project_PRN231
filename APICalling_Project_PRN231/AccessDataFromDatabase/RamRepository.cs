using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class RamRepository
    {
        private static readonly ReviewStoreContext _context = new ReviewStoreContext();

        public static List<Models.Ram> GetAllRam()
        {
            return _context.Rams.ToList();
        }
    }
}
