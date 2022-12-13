using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
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
                    options.Authority = "https://localhost:44386";
                    options.Audience = "WhsWebApi";
                    options.RequireHttpsMetadata = false;
                });

            builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>,ConfigureSwaggerOptions>();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApiVersioning();

            var app = builder.Build();

            InitializeDb(app);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwaggerUI(config =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    config.SwaggerEndpoint( $"/swagger/{description.GroupName}/swagger.json",description.GroupName.ToUpperInvariant());
                    config.RoutePrefix = string.Empty;
                }
            });

            app.UseCustomEcxeptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseApiVersioning();            

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