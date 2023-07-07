using APICalling_Project_PRN231.AccessDataFromDatabase;
using APICalling_Project_PRN231.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICalling_Project_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly IMapper _mapper;
        private RamRepository _ramRepository;

        public RamController(IMapper mapper, RamRepository ramRepository)
        {
            _mapper = mapper;
            _ramRepository = ramRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var listRam = _ramRepository.GetAllRam();
                var result = _mapper.Map<List<RamDTO>>(listRam);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound("Wrongs");
            }
        }
    }
}
