using Interfaces.ContextInterfaces;
using Models;

namespace Repositories.Repositories
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
