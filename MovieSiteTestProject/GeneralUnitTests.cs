using Models;
using Moq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using Interfaces.Interfaces;
using LogicLayer.Logic;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data.ModelData;
using DataLayer.Data;

namespace MovieSiteTestProject
{
    public class GeneralUnitTests
    {
        [Fact]
        public async Task TestClientAsync()
        {
            using (var Client = new HttpClient())
            {
                //Arrange
                var request = "https://localhost:44324/";
                //Act
                var response = await Client.GetAsync(request);
                //Assert             
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
