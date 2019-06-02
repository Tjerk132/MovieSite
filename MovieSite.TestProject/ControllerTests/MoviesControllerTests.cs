using Interfaces.Interfaces;
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
        private Mock<IMoviesContext> mock;
        private Mock<IUserSession> sessionmock;

        private MoviesController controller;
        private readonly MoviesLogic MoviesLogic;
        private Account account;
        public MoviesControllerTests()
        {
            mock = new Mock<IMoviesContext>();
            sessionmock = new Mock<IUserSession>();

            MoviesLogic = new MoviesLogic(mock.Object);
            account = new Account();
        }
        [Theory]
        [InlineData("Simon","123")]
        public void TestGetMovies(string Name, string Password)
        {
            //Arrange
            List<Movie> movies = new List<Movie>
            {
                new Movie(1,"Shrek",DateTime.Now,2000,67),
                new Movie(2,"Shrek 2",DateTime.Now,2000,87),
                new Movie(3,"Shrek 3",DateTime.Now,2000,57),
            };
            mock.Setup(x => x.GetMovies()).Returns(movies);

            account.Name = Name;
            account.Password = Password;

            sessionmock.Setup(x => x.GetAccount()).Returns(account);

            controller = new MoviesController(MoviesLogic, sessionmock.Object);        

            //Act
            ViewResult result = controller.Index() as ViewResult;
            var viewmodel = result.Model as MovieIndexViewModel;
            //Assert
            Assert.Equal(3, viewmodel.Movies.Count);
        }

    }
}
