using Interfaces.ContextInterfaces;
using Moq;
using Xunit;
using Models;
using MovieSite.Controllers;
using MovieSite.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;
using MovieViewer;

namespace MovieSiteTestProject.ControllerTests
{
    public class AccountControllerTests
    {
        private Mock<IAccountContext> accountcontextmock;
        private Mock<IUserSession> sessionmock;

        private AccountController controller;
        private readonly Account account;
        public AccountControllerTests()
        {
            accountcontextmock = new Mock<IAccountContext>();
            sessionmock = new Mock<IUserSession>();

            controller = new AccountController(accountcontextmock.Object, sessionmock.Object);
            account = new Account
            {
                Name = "Simon",
                Password = "333"
            };
        }
        [Fact]
        public void TestLoginExistingAccount()
        {
            //Arrange
            accountcontextmock.Setup(x => x.LoginUser(account))
                .Returns(account);

            //Act
            var result = (RedirectToActionResult)controller.LoginUser(account);

            //Assert
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Movies", result.ControllerName);
        }
        [Fact]
        public void TestLoginNonExistingAccount()
        {
            //Arrange
            accountcontextmock.Setup(x => x.LoginUser(account))
                .Returns(new Account());

            //Act
            var result = controller.LoginUser(account) as ViewResult;
            var viewmodel = result.Model as LoginViewModel;

            //Assert
            Assert.Equal("Wrong account information", viewmodel.Message);
            Assert.Equal("Login", result.ViewName);
        }
    }
}
