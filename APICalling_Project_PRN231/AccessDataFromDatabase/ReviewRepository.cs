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

        public static List<Review> GetReviewByProdId(int productId)
        {
            return _context.Reviews.Where(x => x.ProductId == productId).OrderByDescending(x => x.ReviewDate).ToList();
        }
    }
}
