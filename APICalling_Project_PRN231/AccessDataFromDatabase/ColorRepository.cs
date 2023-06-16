using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class ColorRepository
    {
        private static readonly ReviewStoreContext _context = new ReviewStoreContext();


        public static List<Models.Color> GetAllColor()
        {
            return _context.Colors.ToList();
        }
    }
}
