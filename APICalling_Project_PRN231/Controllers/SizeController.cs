using APICalling_Project_PRN231.AccessDataFromDatabase;
using APICalling_Project_PRN231.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICalling_Project_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {

        private readonly IMapper _mapper;

        public SizeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var listSize = SizeRepository.GetAllSize();
                var result = _mapper.Map<List<SizeDTO>>(listSize);
                return Ok(result);
            }
            catch (Exception)
            {

                return NotFound("Wrongs");
            }
        }
    }
}
