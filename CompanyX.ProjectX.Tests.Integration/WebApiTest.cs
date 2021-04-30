using CompanyX.ProjectX.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CompanyX.ProjectX.Tests.Integration
{
    public class WebApiTest
    {
        private readonly TestServer _server;

        protected HttpClient Client { get; }

        protected JsonSerializerOptions DefaultJsonOptions { get; }

        protected static string NewGuid => Guid.NewGuid().ToString();

        public WebApiTest()
        {
            string webApiRoot = Path.GetDirectoryName(typeof(Startup).Assembly.Location);

            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(webApiRoot)
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(webApiRoot)
                    .AddJsonFile("appsettings.json")
                    .Build())
                .UseStartup<Startup>());

            Client = _server.CreateClient();

            DefaultJsonOptions = CreateJsonSettings();
        }

        protected T Resolve<T>()
        {
            return _server.Host.Services.GetRequiredService<T>();
        }

        private static JsonSerializerOptions CreateJsonSettings()
        {
            JsonSerializerOptions settings = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            settings.Converters.Add(new JsonStringEnumConverter());

            return settings;
        }
    }
}
