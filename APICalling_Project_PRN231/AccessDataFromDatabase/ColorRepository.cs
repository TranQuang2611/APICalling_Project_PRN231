using APICalling_Project_PRN231.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class ColorRepository
    {
        private static readonly ReviewStoreContext _context = new ReviewStoreContext();
        private readonly IMapper _mapper;

        public ColorRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static List<Models.Color> GetAllColor()
        {
            return _context.Colors.ToList();
        }
    }
}
