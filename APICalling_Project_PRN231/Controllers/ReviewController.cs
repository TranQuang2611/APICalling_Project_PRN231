﻿using APICalling_Project_PRN231.AccessDataFromDatabase;
using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

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

        [EnableQuery]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var listReview = _reviewRepository.GetAllReview();
            var result = _mapper.Map<List<ReviewDTO>>(listReview);
            foreach (var item in result)
            {
                item.Comments = _commentRepository.GetPagingCommentByReViewId(item.ReviewId);
            }
            return Ok(result.AsQueryable());
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
            review.IsActive = true;
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
