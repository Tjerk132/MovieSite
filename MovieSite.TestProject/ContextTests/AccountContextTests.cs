using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using MovieViewer;
using Microsoft.AspNetCore.Hosting;
using Interfaces.ContextInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Moq;
using Interfaces.RepositoryInterfaces;

namespace MovieSite.TestProject.ContextTests
{
    public class AccountContextTests
    {
        private readonly TestServer server;
        readonly FakeDbSet<Review> ReviewDbSet;
        private Mock<IAccountContext> contextmock;
        private Account account;

        public AccountContextTests()
        {
            //Create TestServer
            var builder = new WebHostBuilder()
                .UseStartup<Startup>();
            //testserver to initialize connectionstring
            server = new TestServer(builder);

            contextmock = new Mock<IAccountContext>();
            ReviewDbSet = new FakeDbSet<Review>();

            account = new Account
            {
                Name = "Simon",
                Password = "321"
            };

            ReviewDbSet.AddRange(new[]
            {
                new Review(DateTime.Now, "Nice Movie", "Pieter", 4),
                new Review(DateTime.Now, "Excellent Movie", "Sjors", 5)
            });
        }
        [Fact]
        public void TestLoginSucces()
        {
            //Arrange
            contextmock.Setup(x => x.LoginUser(account)).Returns(account);

            //Act
            account = contextmock.Object.LoginUser(account);

            //Assert
            Assert.NotNull(account.Name);
        }
        [Fact]
        public void TestLoginFail()
        {
            //Arrange
            account.Password = "322";
            contextmock.Setup(x => x.LoginUser(account)).Returns(new Account());

            //Act
            account = contextmock.Object.LoginUser(account);

            //Assert
            Assert.Null(account.Name);
        }
        [Fact]
        public void TestGetUserReviews()
        {
            //Arrange
            contextmock.Setup(x => x.GetUserReviews(account)).Returns(ReviewDbSet.GetAll());

            //Act
            List<Review> reviews = contextmock.Object.GetUserReviews(account);

            //Assert
            Assert.Equal(2, reviews.Count);
        }
    }
}
