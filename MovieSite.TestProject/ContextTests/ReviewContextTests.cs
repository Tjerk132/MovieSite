using DataLayer.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Models;
using MovieViewer;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Interfaces.ContextInterfaces;
using Interfaces.RepositoryInterfaces;
using Moq;
using System;

namespace MovieSite.TestProject.ContextTests
{
    public class ReviewContextTests
    {
        private readonly Account account;
        private readonly TestServer server;
        private readonly Mock<IReviewContext> contextmock;
        private readonly FakeDbSet<Review> ReviewDbSet;
        public ReviewContextTests()
        {
            //Create TestServer
            var builder = new WebHostBuilder()
                .UseStartup<Startup>();
            //testserver to initialize connectionstring
            server = new TestServer(builder);

            contextmock = new Mock<IReviewContext>();

            ReviewDbSet = new FakeDbSet<Review>();

            ReviewDbSet.AddRange(new[]
            {
                new Review(DateTime.Now, "Nice movie", "Rick", 4),
                new Review(DateTime.Now, "Cool movie", "Olaf", 5)
            });

            account = new Account
            {
                Name = "Simon"
            };          
        }
        [Fact]
        public void TestGetReviews()
        {
            //Arrange
            contextmock.Setup(x => x.GetReviews(2)).Returns(ReviewDbSet.GetAll());

            //Act
            List<Review> reviews = contextmock.Object.GetReviews(2);

            //Assert
            Assert.Equal(2, reviews.Count);
        }
    }
}
