using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class ReviewRepository
    {
        private static readonly ReviewStoreContext _context = new ReviewStoreContext();

        public static decimal AverageStarByProdId(int productId)
        {
            return Convert.ToDecimal(_context.Reviews.Where(x => x.ProductId == productId).Average(x => x.Rating));
        }

        public static List<Review> GetReviewByProdId(ReviewModel model)
        {
            var query = _context.Reviews.Where(x => x.ProductId == model.ProductId);
            if(model.Star != 0)
            {
                query = query.Where(x => x.Rating == model.Star);
            }
            if(model.Sort != 0)
            {
                query = query.OrderByDescending(x => x.ReviewDate);
            }
            else
            {
                query = query.OrderBy(x => x.ReviewDate);
            }
            return query.ToList();
        }
    }
}
