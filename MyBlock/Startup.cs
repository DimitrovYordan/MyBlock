using System;

using MyBlock.Helpers;
using MyBlock.Repositories;
using MyBlock.Repositories.Interfaces;
using MyBlock.Services;
using MyBlock.Services.Interfaces;
using MyBlock.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

namespace MyBlock
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddDbContext<MyBlockContext>(options =>
            {
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));

                options.EnableSensitiveDataLogging();
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(300);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddHttpContextAccessor();

            // Repositories
            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            // Services
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<IUsersService, UsersService>();

            // Helpers
            services.AddScoped<IMapper, ModelMapper>();
            services.AddScoped<AuthManager>();

            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();

            app.UseSession();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }

    }
}