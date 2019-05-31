using DataLayer.Data;
using Interfaces.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Models;
using Microsoft.AspNetCore.Mvc;
using MovieSite.Controllers;

namespace MovieSiteTestProject
{
    public class UnitTestsAccount
    {
        private Mock<IAccountContext> mock;
        public UnitTestsAccount()
        {
            mock = new Mock<IAccountContext>();
        }

        [Theory]
        [InlineData("Simon", "223")]
        public void TestLoginUser(string Name, string Password)
        {
            var Account = new Account
            {
                Name = Name,
                Password = Password
            };
            var _context = new AccountContext();
            Account account = _context.LoginUser(Account);
            //User cannot login with wrong password
            //(method creates a new instance of the account if user does not exist in database)
            Assert.Null(account.Name);
        }
        [Theory]
        [InlineData("Simon", "223")]
        public void TestRedirectOnLogin(string Name, string Password)
        {
            var Account = new Account
            {
                Name = Name,
                Password = Password
            };

            Account account = new Account();
            mock.Setup(x => x.LoginUser(Account)).Returns(account);

            var controller = new AccountController(mock.Object);
            IActionResult actionResult = controller.LoginUser(Account);

            var ok = actionResult as OkObjectResult;

            Assert.Equal(200, ok.StatusCode);
        }
    }
}
