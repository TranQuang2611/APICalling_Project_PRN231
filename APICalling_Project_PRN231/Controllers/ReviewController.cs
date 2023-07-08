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
    public class ReviewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private CommentRepository _commentRepository;
        private ReviewRepository _reviewRepository;
        private UserRepository _userRepository;

        public ReviewController(IMapper mapper, CommentRepository commentRepository, ReviewRepository reviewRepository, UserRepository userRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult Get(ReviewModel model)
        {
            var listReview = _reviewRepository.GetReviewByProdId(model);
            var result = _mapper.Map<List<ReviewDTO>>(listReview);
            foreach (var item in result)
            {
                item.totalComment = _commentRepository.TotalCommentOfReview(item.ReviewId);
                item.Comments = _commentRepository.GetPagingCommentByReViewId(item.ReviewId);
                item.User = _userRepository.GetUserById(Convert.ToInt32(item.UserId));
            }
            return Ok(result);
        }

        [HttpGet("AddReview")]
        [Authorize]
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
                _reviewRepository.AddReview(review);
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
