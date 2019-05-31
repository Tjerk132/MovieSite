using Interfaces.Interfaces;
using LogicLayer.Logic;
using Models;
using Models.Enumeration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MovieSiteTestProject.LogicTests
{
    public class AccountLogicTests
    {
        private Mock<IAccountContext> _context;
        private AccountLogic logic;
        public AccountLogicTests()
        {
            _context = new Mock<IAccountContext>();
            logic = new AccountLogic(_context.Object);
        }
        [Theory]
        [InlineData("Simon", "223")]
        [InlineData("Alfred", "123")]
        public void TestAdminUser(string Name, string Password)
        {
            Account account = new Account
            {
                Name = Name,
                Password = Password
            };
            _context.Setup(x => x.LoginUser(account))
                .Returns(new Account { Name = account.Name, Password = account.Password, AccountId = 2});

            account = logic.LoginUser(account);
            //Account is only a Admin if AccountId = 1
            Assert.Equal(Priority.User, account.Priority);
        }

    }
}
