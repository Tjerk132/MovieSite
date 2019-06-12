using Interfaces.ContextInterfaces;
using Interfaces.RepositoryInterfaces;
using Models;
using System.Collections.Generic;

namespace Repositories.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IAccountContext _context;
        public AccountRepository(IAccountContext context)
        {
            _context = context;
        }
        public Account LoginUser(Account account)
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
