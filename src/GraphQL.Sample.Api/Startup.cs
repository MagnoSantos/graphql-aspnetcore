using FluentValidation;
using FluentValidation.AspNetCore;
using GraphQL.Sample.Api.Configurations;
using GraphQL.Sample.Domain.GraphQL.Customers.Mutations;

namespace GraphQL.Sample.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IHostEnvironment env)
        {
            var environmentName = env.EnvironmentName;
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{(string.IsNullOrEmpty(environmentName) ? "Development" : environmentName)}.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();

            services.AddControllers();
            services.AddHealthChecks();
            services.ConfigureDataBase();
            services.ConfigureGraphQL();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
            })
                .UseHealthChecks("/healthcheck");
        }
    }
}