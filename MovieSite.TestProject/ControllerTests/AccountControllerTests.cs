﻿using Interfaces.Interfaces;
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
        private Mock<IAccountContext> mock;
        private Mock<IUserSession> sessionmock;

        private AccountController controller;
        private readonly Account account;
        public AccountControllerTests()
        {
            mock = new Mock<IAccountContext>();
            sessionmock = new Mock<IUserSession>();

            controller = new AccountController(mock.Object, sessionmock.Object);
            account = new Account
            {
                Name = "Simon",
                Password = "333"
            };
        }
        [Fact]
        public void TestLogin()
        {
            //Arrange
            mock.Setup(x => x.LoginUser(account))
                .Returns(new Account());
            //Act
            var result = controller.LoginUser(account) as ViewResult;
            var viewmodel = result.Model as LoginViewModel;
            //Assert
            Assert.Equal("Wrong account information", viewmodel.Message);
        }
    }
}
