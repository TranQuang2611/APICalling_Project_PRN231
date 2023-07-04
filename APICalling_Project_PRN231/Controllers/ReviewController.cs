using APICalling_Project_PRN231.AccessDataFromDatabase;
using APICalling_Project_PRN231.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICalling_Project_PRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ReviewController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(ReviewModel model)
        {
            var listReview = ReviewRepository.GetReviewByProdId(model);
            var result = _mapper.Map<List<ReviewDTO>>(listReview);
            foreach (var item in result)
            {
                item.totalComment = CommentRepository.TotalCommentOfReview(item.ReviewId);
                item.Comments = CommentRepository.GetPagingCommentByReViewId(item.ReviewId);
                item.User = UserRepository.GetUserById(Convert.ToInt32(item.UserId));
            }
            return Ok(result);
        }
    }
}
