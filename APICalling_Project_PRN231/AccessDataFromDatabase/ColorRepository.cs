using APICalling_Project_PRN231.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class ColorRepository
    {
        private readonly ReviewStoreContext _context;
        private readonly IMapper _mapper;

        public ColorRepository(IMapper mapper, ReviewStoreContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<Models.Color> GetAllColor()
        {
            return _context.Colors.ToList();
        }
    }
}
