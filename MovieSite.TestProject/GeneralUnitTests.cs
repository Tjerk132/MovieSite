using DataLayer.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieViewer;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MovieSiteTestProject
{
    public class GeneralUnitTests
    {
        private HttpClient Client;
       //private AccountContext _context; //TODO DI

        public GeneralUnitTests()
        {
            var builder = new WebHostBuilder()
           .UseEnvironment("Testing")
           .UseStartup<Startup>();          

            var server = new TestServer(builder);
            //_context = server.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            Client = server.CreateClient();
        }

        [Fact]
        public async Task TestClient()
        {
            //Arrange
            var request = "https://localhost:44305/";
            //Act
            var response = await Client.GetAsync(request);
            //Assert             
            response.EnsureSuccessStatusCode();
        }
        [Theory]
        [InlineData("Movies")]
        [InlineData("Movies/AddMovie")]
        public async Task Test(string url)
        {
            //Arrange
            var request = "https://localhost:44305/";

            //Act
            var response = await Client.GetAsync(request + url);
            string responseHtml = await response.Content.ReadAsStringAsync();

            //Assert 
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains(url, responseHtml);

        }
    }
}
