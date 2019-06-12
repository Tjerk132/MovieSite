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
using Interfaces.RepositoryInterfaces;
using Repositories.Repositories;

namespace MovieViewer
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
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

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
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
            .AddJsonFile($"{env.ContentRootPath}appsettings.json", optional:true)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();

            Configuration = builder.Build();

            var token = Configuration.GetSection("ConnectionStrings:DefaultConnection");
            Connection.ConnectionString = token.Value;

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
