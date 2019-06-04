using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.ContextInterfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Mvc;
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
        private Mock<IRatingContext> ratingcontextmock;
        private Mock<IMoviesContext> moviescontextmock;

        private Mock<IUserSession> sessionmock;

        private Mock<RatingLogic> ratinglogic;
        private Mock<MoviesLogic> movielogic;

        private RatingController controller;
        private readonly Account account;
        private readonly List<Movie> movies;

        public RatingControllerTests()
        {
            sessionmock = new Mock<IUserSession>();
            ratingcontextmock = new Mock<IRatingContext>();
            moviescontextmock = new Mock<IMoviesContext>();

            ratinglogic = new Mock<RatingLogic>(ratingcontextmock.Object);
            movielogic = new Mock<MoviesLogic>(moviescontextmock.Object);

            controller = new RatingController(ratinglogic.Object, movielogic.Object, sessionmock.Object);
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

            movielogic.Setup(x => x.GetMovies()).Returns(movies);

            ratingcontextmock.Setup(x => x.SubmitRating(65, 3, account))
                .Returns("Rating added, Thank you for your feedback");

            //Act
            var result = controller.SubmitRating(65, 3) as ViewResult;
            var viewmodel = result.Model as MovieIndexViewModel;

            //Assert
            Assert.Equal("Rating added, Thank you for your feedback", viewmodel.Message);
            Assert.True(result.ViewName == "Views/Movies/Index.cshtml");
        }
        [Fact]
        public void TestAlreadyRatedMessage()
        {
            //Arrange
            sessionmock.Setup(x => x.GetSession).Returns(account);

            ratingcontextmock.Setup(x => x.SubmitRating(65, 3, account))
                .Returns("You have already rated this movie");

            movielogic.Setup(x => x.GetMovies()).Returns(movies);

            //Act
            var result = controller.SubmitRating(65, 3) as ViewResult;
            var viewmodel = result.Model as MovieIndexViewModel;

            //Assert
            Assert.Equal("You have already rated this movie", viewmodel.Message);
        }
        [Fact]
        public void TestRatingOutOfBounds()
        {
            //Arrange
            sessionmock.Setup(x => x.GetSession).Returns(account);

            movielogic.Setup(x => x.GetMovies()).Returns(movies);

            //Act                 //Rating must be between 0 and 100
            var result = controller.SubmitRating(120, 3) as ViewResult;
            var viewmodel = result.Model as MovieIndexViewModel;

            //Assert
            Assert.Equal("Please insert a rating between 0 and 100", viewmodel.Message);
        }
    }
}
