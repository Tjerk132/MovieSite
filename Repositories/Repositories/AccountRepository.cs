using Interfaces.Interfaces;
using Models;
using System.Collections.Generic;

namespace MovieSite.Repoisitories.Repositories
{
    public class AccountRepository
    {
        private readonly IAccountContext _context;
        public AccountRepository(IAccountContext context)
        {
            _context = context;
        }
        public Account LoginResult(Account account)
        {
            return _context.LoginUser(account);
        }
        public void CreateNew(Account account)
        {
            _context.CreateNew(account);
        }
        public List<Review> GetUserReviews(Account account)
        {
            return _context.GetUserReviews(account);
        }
    }
}
