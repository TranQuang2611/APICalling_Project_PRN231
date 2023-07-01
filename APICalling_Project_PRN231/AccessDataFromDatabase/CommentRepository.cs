using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;

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
            }).ToList();
            return result;
        }
    }
}
