using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.RepositoryInterfaces
{
    public interface IReviewRepository
    {
        void AddReview(Review review, int MovieId);
        List<Review> GetReviews(int MovieId);
    }
}
