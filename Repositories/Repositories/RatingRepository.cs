using Models;
using Interfaces.RepositoryInterfaces;
using Interfaces.ContextInterfaces;

namespace Repositories.Repositories
{
    public class RatingRepository : IRatingRepository
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
