using APICalling_Project_PRN231.AccessDataFromDatabase;
using APICalling_Project_PRN231.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICalling_Project_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMapper _mapper;
        private BrandRepository _brandRepository;

        public BrandController(IMapper mapper, BrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var brands = _brandRepository.GetAllBrand();
                var result = _mapper.Map<List<BrandDTO>>(brands);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound("Wrongs");
            }
        }
    }
}
