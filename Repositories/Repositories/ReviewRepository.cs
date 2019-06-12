using Interfaces.ContextInterfaces;
using Interfaces.RepositoryInterfaces;
using Models;
using System.Collections.Generic;

namespace Repositories.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IReviewContext _context;
        public ReviewRepository(IReviewContext context)
        {
            _context = context;
        }
        public void AddReview(Review review, int MovieId)
        {
            _context.AddReview(review, MovieId);
        }
        public List<Review> GetReviews(int MovieId)
        {
            return _context.GetReviews(MovieId);
        }
    }
}
