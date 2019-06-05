using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.ContextInterfaces;
using Interfaces.LogicInterfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Models;
using Moq;
using MovieSite.Controllers;
using MovieSite.ViewModels.MovieViewModels;
using MovieViewer;
using Xunit;

namespace MovieSite.TestProject.ControllerTests
{
    public class RatingControllerTests
    {
        private Mock<IUserSession> sessionmock;
        private Mock<IRatingLogic> ratinglogic;

        private RatingController controller;
        private readonly Account account;
        private readonly List<Movie> movies;

        public RatingControllerTests()
        {
            sessionmock = new Mock<IUserSession>();
            ratinglogic = new Mock<IRatingLogic>();

            controller = new RatingController(ratinglogic.Object, sessionmock.Object);
            account = new Account
            {
                Name = "Simon",
                Password = "333"
            };

            movies = new List<Movie>
            {
                new Movie(1,"Shrek",DateTime.Now,2000,67),
                new Movie(2,"Shrek 2",DateTime.Now,2000,87),
                new Movie(3,"Transformers",DateTime.Now,2000,57),
            };
        }
        [Fact]
        public void TestRateMovieSucces()
        {
            //Arrange
            sessionmock.Setup(x => x.GetSession).Returns(account);

            ratinglogic.Setup(x => x.SubmitRating(65, 3, account))
                .Returns("Rating added, Thank you for your feedback");

            //Act
            var result = controller.SubmitRating(65, 3) as RedirectToActionResult;

            RouteValueDictionary RouteDictionary = result.RouteValues;
            var Message = RouteDictionary["Message"];

            //Assert
            Assert.Equal("Rating added, Thank you for your feedback", Message);
            Assert.True(result.ActionName == "Index");
            Assert.True(result.ControllerName == "Movies");
        }
        [Fact]
        public void TestAlreadyRatedMessage()
        {
            //Arrange
            sessionmock.Setup(x => x.GetSession).Returns(account);

            ratinglogic.Setup(x => x.SubmitRating(65, 3, account))
                .Returns("You have already rated this movie");

            //Act
            var result = controller.SubmitRating(65, 3) as RedirectToActionResult;

            RouteValueDictionary RouteDictionary = result.RouteValues;
            var Message = RouteDictionary["Message"];

            //Assert
            Assert.Equal("You have already rated this movie", Message);
        }
        [Fact]
        public void TestRatingOutOfBounds()
        {
            //Arrange
            sessionmock.Setup(x => x.GetSession).Returns(account);

            //Act                 //Rating must be between 0 and 100
            var result = controller.SubmitRating(120, 3) as RedirectToActionResult;

            RouteValueDictionary RouteDictionary = result.RouteValues;
            var Message = RouteDictionary["Message"];

            //Assert
            Assert.Equal("Please insert a rating between 0 and 100", Message);
        }
    }
}
