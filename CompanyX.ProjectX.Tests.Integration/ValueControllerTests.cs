using CompanyX.ProjectX.Domain;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace CompanyX.ProjectX.Tests.Integration
{
    public class ValueControllerTests : WebApiTest
    {
        private const string RelativeUrl = "/value";

        [Fact]
        public async Task ExpectedResultIsReturnedOnPostAsync()
        {
            Value input = new() { Text = "testing!" };
            System.Net.Http.HttpResponseMessage response = await Client.PostAsJsonAsync(RelativeUrl, input);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(input, DefaultJsonSettings), responseBody);
        }
    }
}
