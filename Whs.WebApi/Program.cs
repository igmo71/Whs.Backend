using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using Whs.Application;
using Whs.Application.Common.Mappings;
using Whs.Application.Interfaces;
using Whs.Persistence;
using Whs.WebApi.Middleware;

namespace Whs.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IWhsDbContext).Assembly));
            });

            builder.Services.AddApplication();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:44386";
                    options.Audience = "WhsWebApi";
                    options.RequireHttpsMetadata = false;
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitializeDb(app);

            app.UseCustomEcxeptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }

        private static void InitializeDb(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<WhsDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception)
                {
                    // TODO: 
                    throw;
                }
            }
        }
    }
}