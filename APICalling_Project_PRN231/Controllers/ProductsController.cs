using APICalling_Project_PRN231.AccessDataFromDatabase;
using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICalling_Project_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private CommentRepository _commentRepository;
        private ProductRepository _productRepository;
        private ReviewRepository _reviewRepository;
        private UserRepository _userRepository;

        public ProductsController(IMapper mapper, CommentRepository commentRepository, ProductRepository productRepository, UserRepository userRepository, ReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var listProducts = _productRepository.GetListProduct();
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
                var listProducts = _productRepository.GetNewestProduct();
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

        [HttpGet("Feature")]
        public IActionResult GetFeartureProduct()
        {
            try
            {
                var listProducts = _productRepository.GetFeartureProduct();
                if (listProducts.Count > 0)
                {
                    var product = _mapper.Map<List<ProductDTO>>(listProducts);
                    foreach (var item in product)
                    {
                        item.AverageStar = _reviewRepository.AverageStarByProdId(item.ProductId);
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

        [HttpPost("Search")]
        public IActionResult SearchProduct(SearchForm searchForm)
        {
            try
            {
                var listProducts = _productRepository.SearchProduct(searchForm);
                var product = _mapper.Map<List<ProductDTO>>(listProducts);
                foreach (var item in product)
                {
                    item.AverageStar = _reviewRepository.AverageStarByProdId(item.ProductId);
                }
                return Ok(product);

                //var product = ProductRepository.GetProduct();
                //var mapProd = _mapper.Map<ProductDTO>(product);
                //return Ok(mapProd);
            }
            catch (Exception ex)
            {
                return NotFound("Something wrongs");
            }
        }

        [HttpPost("SearchByAdmin")]
        public IActionResult SearchProductByAdmin(SearchForm searchForm)
        {
            try
            {
                var listProducts = _productRepository.SearchProductByAdmin(searchForm);
                var product = _mapper.Map<List<ProductDTO>>(listProducts);
                foreach (var item in product)
                {
                    item.AverageStar = _reviewRepository.AverageStarByProdId(item.ProductId);
                }
                return Ok(product);

                //var product = ProductRepository.GetProduct();
                //var mapProd = _mapper.Map<ProductDTO>(product);
                //return Ok(mapProd);
            }
            catch (Exception ex)
            {
                return NotFound("Something wrongs");
            }
        }

        [HttpGet("Detail")]
        public IActionResult GetDetailProduct(int id)
        {
            try
            {
                var product = _productRepository.GetProductDetail(id);
                var result = _mapper.Map<ProductDTO>(product);
                if(result != null)
                {
                    result.AverageStar = _reviewRepository.AverageStarByProdId(product.ProductId);
                    foreach (var item in result.Reviews)
                    {
                        item.totalComment = _commentRepository.TotalCommentOfReview(item.ReviewId);
                        item.Comments = _commentRepository.GetPagingCommentByReViewId(item.ReviewId);
                        item.User = _userRepository.GetUserById(Convert.ToInt32(item.UserId));
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound("Something wrongs");
            }
        }

        [HttpPost("Update")]
        [Authorize(Roles = "admin")]
        public IActionResult Update(ProductDTO product)
        {
            try
            {
                _productRepository.UpdateProductById(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("Create")]
        [Authorize(Roles = "admin")]
        public IActionResult Create(ProductDTO product)
        {
            try
            {
                var newProd = _productRepository.Create(product);
                product.ProductId = newProd.ProductId;
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("Hide")]
        [Authorize(Roles = "admin")]
        public IActionResult Hide(ProductDTO product)
        {
            try
            {
                var prod = _productRepository.Hide(product);
                product.ProductId = prod.ProductId;
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("CheckProduct")]
        public IActionResult CheckProd(int prodId)
        {
            try
            {
                var listReview = _productRepository.CheckProdHasReview(prodId);
                int countReview = listReview.Count();
                return Ok(countReview);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
