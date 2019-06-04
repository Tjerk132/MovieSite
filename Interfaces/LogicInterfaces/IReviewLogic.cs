using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.LogicInterfaces
{
    public interface IReviewLogic
    {
        void AddReview(Review review, int MovieId);
        List<Review> GetReviews(int MovieId);
        double AverageRating(List<Review> reviews);
        List<double> GetRatingPercentages(List<Review> reviews);
    }
}
