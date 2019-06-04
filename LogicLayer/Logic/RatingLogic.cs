using Models;
using Interfaces.ContextInterfaces;
using Repositories.Repositories;
using Interfaces.LogicInterfaces;

namespace LogicLayer.Logic
{
    public class RatingLogic : IRatingLogic
    {
        private readonly IRatingContext _context;
        public RatingLogic(IRatingContext context)
        {
            _context = context;
            Repository = new RatingRepository(context);
        }
        private RatingRepository Repository { get; }

        public string SubmitRating(int RatingNumber, int MovieId, Account account)
        {
            return Repository.SubmitRating(RatingNumber, MovieId, account);
        }

    }
}