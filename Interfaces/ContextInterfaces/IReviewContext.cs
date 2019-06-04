using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.ContextInterfaces
{
    public interface IReviewContext
    {
        void Add(Review review, int MovieId);
        List<Review> GetReviews(int MovieId);
    }
}
