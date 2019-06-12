using DataLayer.Context;
using Helpers;
using Interfaces.ContextInterfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using MovieViewer;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Interfaces.RepositoryInterfaces;
using Moq;

namespace MovieSite.TestProject.ContextTests
{
    public class RatingContextTests
    {
        private readonly Account account;
        private readonly TestServer server;
        private readonly Mock<IRatingContext> contextmock;
        public RatingContextTests()
        {
            //Create TestServer
            var builder = new WebHostBuilder()
                .UseStartup<Startup>();
            //testserver to initialize connectionstring
            server = new TestServer(builder);

            contextmock = new Mock<IRatingContext>();

            account = new Account
            {
                AccountId = 2
            };
        }
        [Fact]
        public void TestAlreadyRated()
        {
            //Arrange
            contextmock.Setup(x => x.SubmitRating(90, 1, account)).Returns("You have already rated this movie");

            //Act
            string Message = contextmock.Object.SubmitRating(90, 1, account);

            //Assert
            Assert.Equal("You have already rated this movie", Message);
        }
    }
}
