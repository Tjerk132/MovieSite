using Interfaces.Interfaces;
using Models;

namespace MovieSite.Repoisitories.Repositories
{
    public class RatingRepository
    {
        private readonly IRatingContext _context;
        public RatingRepository(IRatingContext context)
        {
            _context = context;
        }
        public string SubmitRating(int RatingNumber, int MovieId, Account account)
        {
            return _context.SubmitRating(RatingNumber, MovieId, account);
        }
    }
}
