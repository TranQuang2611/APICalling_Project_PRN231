using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;

namespace APICalling_Project_PRN231.AccessDataFromDatabase
{
    public class ReviewRepository
    {
        private readonly ReviewStoreContext _context;

        public ReviewRepository(ReviewStoreContext context)
        {
            _context = context;
        }

        public decimal AverageStarByProdId(int productId)
        {
            decimal average = Convert.ToDecimal(_context.Reviews.Where(x => x.ProductId == productId).Average(x => x.Rating));
            return decimal.Round(average, 2);
        }

        public List<Review> GetReviewByProdId(ReviewModel model)
        {
            var query = _context.Reviews.Where(x => x.ProductId == model.ProductId);
            if(model.Star != 0)
            {
                query = query.Where(x => x.Rating == model.Star);
            }
            if(model.Sort != 0)
            {
                query = query.OrderBy(x => x.ReviewDate);
            }
            else
            {
                query = query.OrderByDescending(x => x.ReviewDate);
            }
            return query.ToList();
        }

        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }
    }
}
