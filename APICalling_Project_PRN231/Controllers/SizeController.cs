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
        private SizeRepository _sizeRepository;

        public SizeController(IMapper mapper, SizeRepository sizeRepository)
        {
            _mapper = mapper;
            _sizeRepository = sizeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var listSize = _sizeRepository.GetAllSize();
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
