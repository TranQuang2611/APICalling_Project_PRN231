using APICalling_Project_PRN231.AccessDataFromDatabase;
using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICalling_Project_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var listProducts = ProductRepository.GetListProduct();
                if (listProducts.Count > 0)
                {
                    var product = _mapper.Map<List<ProductDTO>>(listProducts);
                    return Ok(product);
                }
                return NotFound("Not found any product");

                //var product = ProductRepository.GetProduct();
                //var mapProd = _mapper.Map<ProductDTO>(product);
                //return Ok(mapProd);
            }
            catch (Exception ex)
            {
                return NotFound("Something wrongs");
            }
        }

        [HttpGet("Newest")]
        public IActionResult GetNewestProduct()
        {
            try
            {
                var listProducts = ProductRepository.GetNewestProduct();
                if (listProducts.Count > 0)
                {
                    var product = _mapper.Map<List<ProductDTO>>(listProducts);
                    return Ok(product);
                }
                return NotFound("Not found any product");

                //var product = ProductRepository.GetProduct();
                //var mapProd = _mapper.Map<ProductDTO>(product);
                //return Ok(mapProd);
            }
            catch (Exception ex)
            {
                return NotFound("Something wrongs");
            }
        }

        [HttpPost("Search")]
        public IActionResult SearchProduct(SearchForm searchForm)
        {
            try
            {
                var listProducts = ProductRepository.SearchProduct(searchForm);
                if (listProducts.Count > 0)
                {
                    var product = _mapper.Map<List<ProductDTO>>(listProducts);
                    foreach (var item in product)
                    {
                        item.AverageStar = ReviewRepository.AverageStarByProdId(item.ProductId);
                    }
                    return Ok(product);
                }
                return NotFound("Not found any product");

                //var product = ProductRepository.GetProduct();
                //var mapProd = _mapper.Map<ProductDTO>(product);
                //return Ok(mapProd);
            }
            catch (Exception ex)
            {
                return NotFound("Something wrongs");
            }
        }
    }
}
