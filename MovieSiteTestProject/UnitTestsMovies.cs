using Interfaces.Interfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using MovieSite.Controllers;
using MovieSite.Models.ViewModels.MovieViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace MovieSiteTestProject
{
    public class UnitTestsMovies
    {
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

            var  mockhttpcontext = new Mock<ControllerContext>();
           
            MockHttpSession mocksession = new MockHttpSession();
            mockhttpcontext.Setup(x => x.HttpContext.Session).Returns(mocksession);

            Mock<IMoviesContext> mock = new Mock<IMoviesContext>();
            mock.Setup(x => x.GetMovies()).Returns(movies);
            MoviesLogic MoviesLogic = new MoviesLogic(mock.Object);

            MoviesController controller = new MoviesController(MoviesLogic);
            controller.ControllerContext = mockhttpcontext.Object;

            //Act
            ViewResult result = controller.Index() as ViewResult;
            var viewmodel = result.Model as MovieIndexViewModel;
            //Assert
            Assert.Equal(3, viewmodel.Movies.Count);
        }

    }
}
