using Interfaces.Interfaces;
using DataLayer.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data.ModelData
{
    public class ReviewRepository
    {
        private readonly IReviewContext _context;
        public ReviewRepository(IReviewContext context)
        {
            _context = context;
        }
        public void AddReview(Review review, int MovieId)
        {
            _context.Add(review, MovieId);
        }
        public List<Review> GetReviews(int MovieId)
        {
            return _context.GetReviews(MovieId);
        }
    }
}
