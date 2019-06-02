using Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using Interfaces.Interfaces;
using LogicLayer.Logic;
using DataLayer.Context;
using DataLayer.Context;

namespace MovieSiteTestProject
{
    public class GeneralUnitTests
    {
        [Fact]
        public async Task TestClient()
        {
            using (var Client = new HttpClient())
            {
                //Arrange
                var request = "https://localhost:44305/";
                //Act
                var response = await Client.GetAsync(request);
                //Assert             
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
