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
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MovieSite.TestProject.ContextTests
{
    public class MovieContextTests
    {
        private readonly TestServer server;
        readonly FakeDbSet<Movie> MovieDbSet;
        private Mock<IMoviesContext> contextmock;
        public MovieContextTests()
        {
            //Create TestServer
            var builder = new WebHostBuilder()
                .UseStartup<Startup>();
            //testserver to initialize connectionstring
            server = new TestServer(builder);

            contextmock = new Mock<IMoviesContext>();
            MovieDbSet = new FakeDbSet<Movie>();
            MovieDbSet.AddRange(new[] 
            {
                new Movie(1, "Shrek", DateTime.Now, 2000, 67),
                new Movie(2, "Shrek 2", DateTime.Now, 1500, 64),
                new Movie(3, "Shrek 3", DateTime.Now, 1000, 62)
            });
        }
        [Fact]
        public void TestAddMovie()
        {
            //Arrange
            Movie movie = new Movie("Shrek", DateTime.Now);
            contextmock.Setup(x => x.AddMovie(movie)).Returns(movie);

            //Act
            movie = contextmock.Object.AddMovie(movie);

            //Assert
            Assert.Equal("Shrek", movie.Title);
            Assert.Equal(0, movie.Rating);
        }
        [Fact]
        public void TestRemoveMovie()
        {
            //Remove test for possible context RemoveMethod in the future

            //Act
            MovieDbSet.RemoveAt(2);     

            //Assert
            Assert.True(MovieDbSet.Where(x => x.Title == "Shrek 3").Count() == 0);
        }
        [Fact]
        public void TestGetMovies()
        {
            //Arrange
            contextmock.Setup(x => x.GetMovies()).Returns(MovieDbSet.GetAll());

            //Act
            var movies = contextmock.Object.GetMovies();

            //Assert
            Assert.Equal(3, movies.Count());
        }
    }
}
