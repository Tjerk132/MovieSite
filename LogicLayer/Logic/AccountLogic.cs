using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Security.Cryptography;
using DataLayer.Data;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using DataLayer.Data.ModelData;
using Models;
using Interfaces.Interfaces;
using Models.Enumeration;

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
            Repository.CreateNew(account);
        }
    }
}