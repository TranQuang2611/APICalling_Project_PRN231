using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class CommentRepository
    {
        private readonly ReviewStoreContext _context;
        private UserRepository _userRepository;

        public CommentRepository(ReviewStoreContext context, UserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public List<CommentDTO> GetListCommentByReViewId(int reviewId)
        {
            var list = _context.Comments.Where(x => x.ReviewId == reviewId).ToList();
            var result = list.Select(x => new CommentDTO
            {
                ReviewId = x.ReviewId,
                CommentDate = x.CommentDate,
                LikeReact = x.LikeReact,
                UserId = x.UserId,
                Comment1 = x.Comment1,
                User = _userRepository.GetUserById(Convert.ToInt32(x.UserId))
            }).OrderByDescending(r => r.CommentDate).ToList();
            return result;
        }

        public List<CommentDTO> GetPagingCommentByReViewId(int reviewId)
        {
            var list = _context.Comments.Where(x => x.ReviewId == reviewId).ToList();
            var result = list.Select(x => new CommentDTO
            {
                ReviewId = x.ReviewId,
                CommentId = x.CommentId,
                CommentDate = x.CommentDate,
                LikeReact = x.LikeReact,
                UserId = x.UserId,
                Comment1 = x.Comment1,
                User = _userRepository.GetUserById(Convert.ToInt32(x.UserId))
            }).OrderByDescending(r => r.CommentDate).ToList();
            return result;
        }

        public int TotalCommentOfReview(int reviewId)
        {
            return _context.Comments.Where(x => x.ReviewId == reviewId).Count();
        }
        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public Comment UpdateComment(int commentId, string content)
        {
            var comment =  _context.Comments.FirstOrDefault(x => x.CommentId == commentId);
            comment.Comment1 = content.Trim();
            comment.CommentDate = DateTime.Now;
            _context.Comments.Update(comment);
            _context.SaveChanges(true);
            return comment;
        }

        public void DeleteComment(int commentId)
        {
            var comment = _context.Comments.FirstOrDefault(x => x.CommentId == commentId);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
