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
    public class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;

        public CommentController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("AddComment")]
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
                CommentRepository.AddComment(comment);
                commentDTO = _mapper.Map<CommentDTO>(comment);
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
