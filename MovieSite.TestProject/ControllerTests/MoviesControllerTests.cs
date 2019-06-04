using Interfaces.ContextInterfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using MovieSite.Controllers;
using MovieSite.ViewModels.MovieViewModels;
using MovieViewer;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MovieSiteTestProject.ControllerTests
{
    public class MoviesControllerTests
    {
        private Mock<IMoviesContext> moviecontextmock;
        private Mock<IUserSession> sessionmock;

        private MoviesController controller;
        private readonly MoviesLogic MoviesLogic;
        private Account account;

        private readonly List<Movie> movies;
        public MoviesControllerTests()
        {
            moviecontextmock = new Mock<IMoviesContext>();
            sessionmock = new Mock<IUserSession>();
            MoviesLogic = new MoviesLogic(moviecontextmock.Object);
            account = new Account();
            controller = new MoviesController(MoviesLogic, sessionmock.Object);

            movies = new List<Movie>
            {
                new Movie(1,"Shrek",DateTime.Now,2000,67),
                new Movie(2,"Shrek 2",DateTime.Now,2000,87),
                new Movie(3,"Transformers",DateTime.Now,2000,57),
            };
        }
        [Theory]
        [InlineData("Simon","123")]
        public void TestGetMovies(string Name, string Password)
        {
            //Arrange
            moviecontextmock.Setup(x => x.GetMovies()).Returns(movies);

            account.Name = Name;
            account.Password = Password;

            sessionmock.Setup(x => x.GetSession).Returns(account);    

            //Act
            ViewResult result = controller.Index() as ViewResult;
            var viewmodel = result.Model as MovieIndexViewModel;

            //Assert
            Assert.Equal(3, viewmodel.Movies.Count);
        }
        [Fact]
        public void TestNotLoggedIn()
        {
            //Arrange
            Account Account = null;
            sessionmock.Setup(x => x.GetSession).Returns(Account);
            
            //Act
            var result = controller.Index() as ViewResult;
   
            //Assert
            Assert.Equal("NotLoggedIn", result.ViewName);
        }
        [Fact]
        public void TestFilterMovies()
        {
            //Arrange
            moviecontextmock.Setup(x => x.GetMovies()).Returns(movies);

            //Act
            ViewResult result = controller.FilterMovies("Shrek") as ViewResult;
            var viewmodel = result.Model as MovieIndexViewModel;

            //Assert
            Assert.Equal(2, viewmodel.Movies.Count);
        }
        [Fact]
        public void TestFilterNoMoviesFound()
        {
            //Arrange
            moviecontextmock.Setup(x => x.GetMovies()).Returns(movies);

            //Act
            ViewResult result = controller.FilterMovies("Batman") as ViewResult;
            var viewmodel = result.Model as MovieIndexViewModel;

            //Assert
            Assert.Equal("No movies found", viewmodel.Message);
        }

    }
}
