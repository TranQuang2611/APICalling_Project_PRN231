using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;
using Microsoft.EntityFrameworkCore;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class CommentRepository
    {
        private static readonly ReviewStoreContext _context = new ReviewStoreContext();

        public static List<CommentDTO> GetListCommentByReViewId(int reviewId)
        {
            var list = _context.Comments.Where(x => x.ReviewId == reviewId).ToList();
            var result = list.Select(x => new CommentDTO
            {
                ReviewId = x.ReviewId,
                CommentDate = x.CommentDate,
                LikeReact = x.LikeReact,
                UserId = x.UserId,
                Comment1 = x.Comment1,
                User = UserRepository.GetUserById(Convert.ToInt32(x.UserId))
            }).OrderByDescending(r => r.CommentDate).ToList();
            return result;
        }

        public static List<CommentDTO> GetPagingCommentByReViewId(int reviewId)
        {
            var list = _context.Comments.Where(x => x.ReviewId == reviewId).ToList();
            var result = list.Select(x => new CommentDTO
            {
                ReviewId = x.ReviewId,
                CommentDate = x.CommentDate,
                LikeReact = x.LikeReact,
                UserId = x.UserId,
                Comment1 = x.Comment1,
                User = UserRepository.GetUserById(Convert.ToInt32(x.UserId))
            }).OrderByDescending(r => r.CommentDate).Take(3).ToList();
            return result;
        }

        public static int TotalCommentOfReview(int reviewId)
        {
            return _context.Comments.Where(x => x.ReviewId == reviewId).Count();
        }
        public static void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
