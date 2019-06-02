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
        private Mock<IAccountContext> accountcontextmock;
        private AccountLogic logic;
        private Account account;
        public AccountLogicTests()
        {
            accountcontextmock = new Mock<IAccountContext>();
            logic = new AccountLogic(accountcontextmock.Object);
            account = new Account();
        }
        [Theory]
        [InlineData("Simon", "223")]
        [InlineData("Alfred", "123")]
        public void TestAdminUser(string Name, string Password)
        {
            //Arrange
            account.Name = Name;
            account.Password = Password;

            accountcontextmock.Setup(x => x.LoginUser(account))
                .Returns(new Account { Name = account.Name, Password = account.Password, AccountId = 2});

            //Act
            account = logic.LoginUser(account);

            //Account is only a Admin if AccountId = 1
            //Assert
            Assert.Equal(Priority.User, account.Priority);
        }

    }
}
