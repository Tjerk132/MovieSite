using System;
using DataLayer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LogicLayer.Logic;
using Helpers;
using Interfaces.ContextInterfaces;
using Interfaces.LogicInterfaces;
using MovieSite.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MovieViewer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        public static string ConnectionString { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddSessionStateTempDataProvider();

            services.AddSession();
            services.AddMvc();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserSession, UserSession>();

            services.AddScoped<IAccountLogic, AccountLogic>();
            services.AddScoped<IMoviesLogic, MoviesLogic>();
            services.AddScoped<IReviewLogic, ReviewLogic>();
            services.AddScoped<IRatingLogic, RatingLogic>();

            services.AddScoped<IAccountContext, AccountContext>();
            services.AddScoped<IMoviesContext, MovieContext>();
            services.AddScoped<IReviewContext, ReviewContext>();
            services.AddScoped<IRatingContext, RatingContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStatusCodePagesWithReExecute("/Error/Error", "?statusCode={0}");

            app.UseStaticFiles();

            app.UseSession();

            app.UseMvc();

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json")
            .Build();

            ConnectionString = builder["ConnectionStrings:DefaultConnection"];
            Connection.ConnectionString = ConnectionString;
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
