using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyCvProject.Infra.Data.Context;
using MyCvProject.Infra.IoC.DependencyInjections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyCvProject.Domain.Consts;

namespace MyCvProject.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region DataBase Context

            services.AddDbContext<MyCvProjectContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("MyCvProjectConnection"));
                }, ServiceLifetime.Transient
            );

            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyCvProject.Api", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), "MyCvProject.Api.xml"));
            });

            #region Add IoC

            RegisterServices(services);

            #endregion

            #region JWT

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Const.SiteUrl,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.VerifyCodeJwt))
                    };
                });

            services.AddCors(options =>
            {

                options.AddPolicy("EnableCors", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    //.AllowCredentials()
                    .Build();
                });
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyCvProject.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static void RegisterServices(IServiceCollection service)
        {
            DependencyContainer.RegisterServices(service);
        }
    }
}
