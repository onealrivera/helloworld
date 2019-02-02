using System;
using System.IO;
using System.Reflection;

using HelloWorld.Interface;
using HelloWorld.Store;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloWorld.Api
{
    /// <summary>
    /// Web start up
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public Startup(IConfiguration configuration)
            => Configuration = configuration;
        
        /// <summary>
        /// Configure services available for this application
        /// </summary>
        /// <param name="services">Services collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IStore>(Factory.GetStore())
                .AddVersionedApiExplorer( o =>
                {
                    o.GroupNameFormat = "'v'VVV";
                    o.SubstituteApiVersionInUrl = true;
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                })
                .AddApiVersioning(o =>
                {
                    o.ReportApiVersions = true;
                    o.ApiVersionReader = new UrlSegmentApiVersionReader();
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                    o.UseApiBehavior = true;
                })
                .AddSwaggerGen(c =>
                {
                    var provider = services.BuildServiceProvider()
                           .GetService<IApiVersionDescriptionProvider>();

                    foreach (var version in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerDoc(version.GroupName, new Swashbuckle.AspNetCore.Swagger.Info()
                        {
                            Title = "Hello World Api",
                            Version = version.GroupName
                        });
                    }

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });
        }

        /// <summary>
        /// Configures the application pipeline
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Hosting environment</param>
        /// <param name="provider">Api version description provider</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger(o =>
            {
                o.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    if (httpReq.PathBase.HasValue)
                    {
                        swaggerDoc.BasePath = httpReq.PathBase.Value;
                    }
                });
            })
            .UseSwaggerUI(c =>
            {
                foreach (var version in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"{version.GroupName}/swagger.json",
                        version.GroupName.ToUpperInvariant());
                }

            });
        }
    }
}
