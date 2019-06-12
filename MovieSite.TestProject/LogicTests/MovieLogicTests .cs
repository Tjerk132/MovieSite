using Interfaces.ContextInterfaces;
using Interfaces.RepositoryInterfaces;
using LogicLayer.Logic;
using Models;
using Models.Enumeration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MovieSiteTestProject.LogicTests
{
    public class MoviesLogicTests
    {
        private Mock<IMoviesRepository> repositorymock;
        private MoviesLogic logic;
        public MoviesLogicTests()
        {
            repositorymock = new Mock<IMoviesRepository>();
            logic = new MoviesLogic(repositorymock.Object);
        }
        [Fact]
        public void TestFilterMovies()
        {
            //Arrange
            List<Movie> movies = new List<Movie>
            {
                new Movie(1,"Shrek",DateTime.Now,2000,67),
                new Movie(2,"Shrek 2",DateTime.Now,2000,62),
                new Movie(3,"Transformers",DateTime.Now,2000,87),
                new Movie(4,"Harry Potter",DateTime.Now,2000,57),
            };

            //Act
            movies = logic.Filtermovie(movies, "Shrek");

            //Assert
            Assert.Equal(2, movies.Count);
        }

    }
}
