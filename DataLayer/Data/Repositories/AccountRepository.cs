using Interfaces.Interfaces;
using DataLayer.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data.ModelData
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
            return _context.LoginResult(account);
        }
        public void CreateNew(Account account)
        {
            _context.CreateNew(account);
        }
    }
}
