using System.Collections.Generic;
using Models;
using Interfaces.Interfaces;
using Models.Enumeration;
using Helpers;
using MovieSite.Repoisitories.Repositories;

namespace LogicLayer.Logic
{
    public class AccountLogic
    {
        public AccountLogic(IAccountContext context)
        {
            Repository = new AccountRepository(context);
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
            account.passwordhash = PasswordHelper.HashPassword(account.Password);
            Repository.CreateNew(account);
        }
        public List<Review> GetUserReviews(Account account)
        {
            return Repository.GetUserReviews(account);
        }
    }
}