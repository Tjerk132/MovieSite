using Models;
using Moq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using Interfaces.Interfaces;
using LogicLayer.Logic;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data.ModelData;
using DataLayer.Data;
using MovieSite.Controllers;

namespace MovieSiteTestProject
{
    public class UnitTestsAccountContext
    {
        private readonly AccountContext context;
        private readonly AccountLogic _logic;
        AccountController _controller;
        public UnitTestsAccountContext()
        {           
            context = new AccountContext();
            _controller = new AccountController(_logic);
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
            Account account = context.LoginUser(Account);
            //User cannot login with wrong password
            //(method creates a new instance of the account if user does not exist in database)
            Assert.Null(account.Name);
            

            //var MockAccountService = new Mock<IAccountContext>();
            //MockAccountService
            //   .Setup(x => x.LoginUser(Account))
            //   .Returns(Account);
            
            //var accountlogic = new AccountLogic(MockAccountService.Object);
            //Account output = accountlogic.LoginUser(Account);

            ////User can login
            //MockAccountService.VerifyAll();
            
        }   
    }
}
