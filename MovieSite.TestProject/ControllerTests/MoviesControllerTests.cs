using Interfaces.Interfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using MovieSite.Controllers;
using MovieSite.Models.ViewModels.MovieViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MovieSiteTestProject.ControllerTests
{
    public class MoviesControllerTests
    {
        private Mock<IMoviesContext> mock;
        public MoviesControllerTests()
        {
            mock = new Mock<IMoviesContext>();
        }
        [Fact]
        public void TestGetMovies()
        {
            //Arrange
            List<Movie> movies = new List<Movie>
            {
                new Movie(1,"Shrek",DateTime.Now,2000,67),
                new Movie(2,"Shrek 2",DateTime.Now,2000,87),
                new Movie(3,"Shrek 3",DateTime.Now,2000,57),
            };
            var Account = new Account
            {
                Name = "Simon",
                Password = "123"
            };

            mock.Setup(x => x.GetMovies()).Returns(movies);
            MoviesLogic MoviesLogic = new MoviesLogic(mock.Object);


            var controllercontext = new Mock<HttpContext>();
            controllercontext.Setup(x => x.Session.GetObject<Account>("User")).Returns(Account);

            MoviesController controller = new MoviesController(MoviesLogic);
            controller.ControllerContext.HttpContext = controllercontext.Object;

            //Act
            ViewResult result = controller.Index() as ViewResult;
            var viewmodel = result.Model as MovieIndexViewModel;
            //Assert
            Assert.Equal(3, viewmodel.Movies.Count);
        }

    }
}
