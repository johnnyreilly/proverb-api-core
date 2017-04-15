using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Proverb.Data.EntityFramework;
using Proverb.Data.EntityFramework.CommandQuery;
using Proverb.Web.Helpers;

namespace Proverb.Api.Core
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            var connectionString = Configuration["SecretConnectionString"] ?? Configuration.GetConnectionString("ProverbConnection");

            services.AddDbContext<ProverbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<ISageCommand, SageCommand>();
            services.AddScoped<ISageQuery, SageQuery>();
            services.AddScoped<ISayingQuery, SayingQuery>();
            services.AddScoped<ISayingCommand, SayingCommand>();
            services.AddScoped<IUserQuery, UserQuery>();
            services.AddScoped<IUserCommand, UserCommand>();

            services.AddSingleton<IAppConfigHelper, AppConfigHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
