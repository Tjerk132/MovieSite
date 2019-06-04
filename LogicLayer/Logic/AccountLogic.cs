using System.Collections.Generic;
using Models;
using Interfaces.ContextInterfaces;
using Models.Enumeration;
using Helpers;
using Repositories.Repositories;
using Interfaces.LogicInterfaces;

namespace LogicLayer.Logic
{
    public class AccountLogic : IAccountLogic
    {
        private readonly IAccountContext _context;
        public AccountLogic(IAccountContext context)
        {
            _context = context;
            Repository = new AccountRepository(_context);
        }
        private AccountRepository Repository { get; }

        public Account LoginUser(Account account)
        {
            account = Repository.LoginResult(account);
            if (account.AccountId == 1)
            {
                account.Priority = Priority.Admin;
            }
            else account.Priority = Priority.User;

            return account;
        }
        public void CreateNew(Account account)
        {
            PasswordHelper passwordHelper = new PasswordHelper();
            account.passwordhash = passwordHelper.HashPassword(account.Password);
            Repository.CreateNew(account);
        }
        public List<Review> GetUserReviews(Account account)
        {
            return Repository.GetUserReviews(account);
        }
    }
}