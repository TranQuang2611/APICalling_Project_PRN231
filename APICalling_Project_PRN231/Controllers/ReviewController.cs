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
    public class ReviewController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ReviewController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
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

        [HttpGet("AddReview")]
        public IActionResult AddReview(int prodId, int userReviewId, string content, int rating)
        {
            Review review = new Review();
            review.ReviewDate = DateTime.Now;
            review.ProductId = prodId;
            review.UserId = userReviewId;
            review.Content = content;
            review.Rating = rating;
            review.LikeReact = 0;
            ReviewDTO reviewDTO = new ReviewDTO();
            try
            {
                ReviewRepository.AddReview(review);
                reviewDTO = _mapper.Map<ReviewDTO>(review);
                return Ok(reviewDTO);
            }
            catch (Exception)
            {
                return Ok(reviewDTO);
            }

        }
    }
}
