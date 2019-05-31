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
using MovieSite.Models.ViewModels.AccountViewModels;

namespace MovieSiteTestProject.ControllerTests
{
    public class AccountControllerTests
    {
        private Mock<IAccountContext> mock;
        private AccountController controller;
        public AccountControllerTests()
        {
            mock = new Mock<IAccountContext>();
            controller = new AccountController(mock.Object);
        }
        [Theory]
        [InlineData("Simon","333")]
        public void TestLogin(string Name, string Password)
        {
            //Arrange
            Account account = new Account
            {
                Name = Name,
                Password = Password
            };
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
