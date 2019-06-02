using Models;
using Interfaces.Interfaces;
using MovieSite.Repoisitories.Repositories;

namespace LogicLayer.Logic
{
    public class RatingLogic
    {
        public RatingLogic(IRatingContext context)
        {
            Repository = new RatingRepository(context);
        }
        private RatingRepository Repository { get; }

        public string SubmitRating(int RatingNumber, int MovieId, Account account)
        {
            return Repository.SubmitRating(RatingNumber, MovieId, account);
        }

    }
}