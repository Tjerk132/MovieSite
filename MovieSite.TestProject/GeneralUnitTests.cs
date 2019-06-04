using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

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
