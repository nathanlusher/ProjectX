using CompanyX.ProjectX.Domain.Interfaces;
using CompanyX.ProjectX.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace CompanyX.ProjectX.WebApi.ExtensionMethods
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private const string SwaggerDocName = "v1";

        /// <summary>
        /// Configures the swagger middleware for API documentation.
        /// </summary>
        /// <param name="services">The services collection.</param>
        public static void ConfigureSwaggerMiddleware(this IServiceCollection services)
        {
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerDocName, CreateApiInfo(assemblyName));
                AddXmlSummaries(assemblyName, c);
            });
        }

        /// <summary>
        /// Injects the appropriate dependencies required by the API.
        /// </summary>
        /// <param name="services">The services collection.</param>
        public static void InjectDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IValueService, ValueService>();
        }

        private static OpenApiInfo CreateApiInfo(AssemblyName assemblyName)
        {
            return new OpenApiInfo
            {
                Title = assemblyName.Name,
                Version = assemblyName.Version.ToString()
            };
        }

        private static void AddXmlSummaries(AssemblyName assemblyName, SwaggerGenOptions options)
        {
            string xmlFile = $"{assemblyName.Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }
    }
}
