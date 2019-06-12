using Interfaces.LogicInterfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Moq;
using MovieSite.Controllers;
using MovieSite.ViewModels.AccountViewModels;
using MovieSite.ViewModels.MovieViewModels;
using MovieViewer;
using Xunit;

namespace MovieSite.TestProject.Integration_Tests
{
    public class TestIntegrations
    {
        private TestServer server;

        private MoviesController moviescontroller;
        private AccountController accountcontroller;

        private readonly IMoviesLogic movieslogic;
        private readonly IAccountLogic accountlogic;
        private readonly Mock<IUserSession> sessionlogic;

        private readonly Account account;

        public TestIntegrations()
        {
            //Create TestServer
            var builder = new WebHostBuilder()
                .UseStartup<Startup>();

            server = new TestServer(builder);

            //Get individual scopes from testserver to create controllerinstances with scoped services
            movieslogic = server.Host.Services.GetService<IMoviesLogic>();
            accountlogic = server.Host.Services.GetService<IAccountLogic>();

            sessionlogic = new Mock<IUserSession>();

            account = new Account
            {
                Name = "Simon",
                Password = "123"
            };
            //Inject services from testserver into the controllers
            moviescontroller = new MoviesController(movieslogic, sessionlogic.Object);
            accountcontroller = new AccountController(accountlogic, sessionlogic.Object);
        }
        [Fact]
        public void TestFilterMoviesPath()
        {
            //Arrange
            //Sessionlogic must be mocked because user session is not created when directly calling filtermovies actionresult
            sessionlogic.Setup(x => x.GetSession).Returns(account);

            //Act
            var result =  moviescontroller.FilterMovies("Batman") as ViewResult;
            var viewmodel = result.Model as MovieIndexViewModel;

            //Assert
            Assert.Equal("No movies found", viewmodel.Message);
        }
        [Fact]
        public void TestLoginUserPath()
        {
            //Act
            var result = accountcontroller.LoginUser(account) as ViewResult;
            var viewmodel = result.Model as LoginViewModel;

            //Assert
            Assert.Equal("Wrong account information", viewmodel.Message);
        }
    }
}

