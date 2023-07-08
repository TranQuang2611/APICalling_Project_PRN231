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
    public class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private CommentRepository _commentRepository;
        private UserRepository _userRepository;

        public CommentController(IMapper mapper, CommentRepository commentRepository, UserRepository userRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        [HttpGet("AddComment")]
        [Authorize]
        public IActionResult AddComment(int reviewId, int userCmtId, string content)
        {
            Comment comment = new Comment();
            comment.ReviewId = reviewId;
            comment.UserId = userCmtId;
            comment.LikeReact = 0;
            comment.CommentDate = DateTime.Now;
            comment.Comment1 = content;
            CommentDTO commentDTO = new CommentDTO();
            try
            {
                _commentRepository.AddComment(comment);
                commentDTO = _mapper.Map<CommentDTO>(comment);
                commentDTO.UserName = _userRepository.GetUserById(userCmtId).Username;
                return Ok(commentDTO);
            }
            catch (Exception)
            {
                commentDTO.Message = "Có lỗi xảy ra";
                return Ok(commentDTO);
            }
            
        }
    }
}
