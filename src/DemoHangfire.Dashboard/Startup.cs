using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using DemoHangfire.Helpers;

namespace DemoHangfire.Dashboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var cs = Environment.GetEnvironmentVariable("ConnectionString"); //Fetch the connection string from the environment variable defined in docker-compose.yml. If you do not want to use docker, edit this to be relevant to you.
            SqlServerHelper.WaitForSqlServer(cs); //Make sure SQL SErver is started before going further.

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(cs)
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Redirect the root to Hangfire's dashboard and allow everyone to access it.
            app.UseHangfireDashboard("", new DashboardOptions
            {
                Authorization = new[] { new LaxAuthorizationFilter() }
            });
        }
    }
}
