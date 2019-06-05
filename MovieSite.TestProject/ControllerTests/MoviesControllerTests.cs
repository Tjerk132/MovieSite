using Interfaces.ContextInterfaces;
using Interfaces.LogicInterfaces;
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
        private Mock<IMoviesLogic> logicmock;
        private Mock<IMoviesContext> contextmock;
        private Mock<IUserSession> sessionmock;

        private MoviesController controller;
        private readonly MoviesLogic MoviesLogic;
        private Account account;

        private readonly List<Movie> movies;
        public MoviesControllerTests()
        {
            logicmock = new Mock<IMoviesLogic>();
            contextmock = new Mock<IMoviesContext>();
            sessionmock = new Mock<IUserSession>();

            MoviesLogic = new MoviesLogic(contextmock.Object);
            account = new Account();
            controller = new MoviesController(logicmock.Object, sessionmock.Object);

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
            logicmock.Setup(x => x.GetMovies()).Returns(movies);

            account.Name = Name;
            account.Password = Password;

            sessionmock.Setup(x => x.GetSession).Returns(account);    

            //Act
            ViewResult result = controller.Index("") as ViewResult;
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
            var result = controller.Index("") as ViewResult;
   
            //Assert
            Assert.Equal("NotLoggedIn", result.ViewName);
        }
        [Fact]
        public void TestFilterMovies()
        {
            //Arrange
            logicmock.Setup(x => x.GetMovies()).Returns(movies);
            var filteredmovies = movies.FindAll(x => x.Title.Contains("Shrek"));
            logicmock.Setup(x => x.Filtermovie(movies, "Shrek")).Returns(filteredmovies);

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
            logicmock.Setup(x => x.GetMovies()).Returns(movies);
            var filteredmovies = movies.FindAll(x => x.Title.Contains("Batman"));
            logicmock.Setup(x => x.Filtermovie(movies, "Batman")).Returns(filteredmovies);
            //Act
            ViewResult result = controller.FilterMovies("Batman") as ViewResult;
            var viewmodel = result.Model as MovieIndexViewModel;

            //Assert
            Assert.Equal("No movies found", viewmodel.Message);
        }

    }
}
