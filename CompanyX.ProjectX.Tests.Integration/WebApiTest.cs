using CompanyX.ProjectX.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;

namespace CompanyX.ProjectX.Tests.Integration
{
    public class WebApiTest
    {
        private readonly TestServer _server;

        protected HttpClient Client { get; }

        protected JsonSerializerSettings DefaultJsonSettings { get; }

        public WebApiTest()
        {
            _server = new TestServer(new WebHostBuilder()
           .UseStartup<Startup>());
            Client = _server.CreateClient();

            DefaultJsonSettings = CreateJsonSettings();
        }

        private static JsonSerializerSettings CreateJsonSettings()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
        }
    }
}
