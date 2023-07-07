using APICalling_Project_PRN231.AccessDataFromDatabase;
using APICalling_Project_PRN231.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICalling_Project_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ColorRepository _colorRepository;

        public ColorController(IMapper mapper, ColorRepository colorRepository)
        {
            _mapper = mapper;
            _colorRepository = colorRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var listColor = _colorRepository.GetAllColor();
                var result = _mapper.Map<List<ColorDTO>>(listColor);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound("Wrongs");
            }
        }
    }
}
