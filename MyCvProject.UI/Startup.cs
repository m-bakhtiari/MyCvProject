using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCvProject.Infra.Data.Context;
using MyCvProject.Infra.IoC.DependencyInjections;
using System;
using MyCvProject.Core.Convertors;

namespace MyCvProject.UI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x => x.EnableEndpointRouting = false);

            #region Add IoC

            RegisterServices(services);
            services.AddTransient<IViewRenderService, RenderViewToString>();

            #endregion

            #region Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);

            });

            #endregion

            #region DataBase Context

            services.AddDbContext<MyCvProjectContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("MyCvProjectConnection"));
                },ServiceLifetime.Transient
            );

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"

                );
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        public static void RegisterServices(IServiceCollection service)
        {
            DependencyContainer.RegisterServices(service);
        }
    }
}
