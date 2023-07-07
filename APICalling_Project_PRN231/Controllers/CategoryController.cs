using APICalling_Project_PRN231.AccessDataFromDatabase;
using APICalling_Project_PRN231.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICalling_Project_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private CategoryRepository _categoryRepository;

        public CategoryController(IMapper mapper, CategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var categories = _categoryRepository.GetAllCategory();
                var result = _mapper.Map<List<CategoryDTO>>(categories);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound("Wrongs");
            }
        }
    }
}
