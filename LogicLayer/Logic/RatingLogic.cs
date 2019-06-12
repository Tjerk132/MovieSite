using Models;
using Interfaces.ContextInterfaces;
using Repositories.Repositories;
using Interfaces.LogicInterfaces;
using Interfaces.RepositoryInterfaces;

namespace LogicLayer.Logic
{
    public class RatingLogic : IRatingLogic
    {
        private readonly IRatingRepository Repository;
        public RatingLogic(IRatingRepository repository)
        {
            Repository = repository;
        }
        public string SubmitRating(int RatingNumber, int MovieId, Account account)
        {
            return Repository.SubmitRating(RatingNumber, MovieId, account);
        }

    }
}