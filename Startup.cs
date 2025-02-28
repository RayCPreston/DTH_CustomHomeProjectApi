using System.Text.Json.Serialization;
using DTH.API.Data;
using DTH.API.Middleware;
using DTH.API.Services.HomeProjectServices;
using DTH.API.Services.UserServices;
using DTH.API.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace DTH.API
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
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            services.AddDbContext<HomeProjectDbContext>(options =>
                options.UseSqlite("Data Source=HomeProjects.db"));
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlite("Data Source=Users.db"));

            services.AddLogging();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DTH API",
                    Version = "v1.0",
                    Description = "API documentation for DTH project"
                });
            });

            services.AddScoped<CreateUserService>();
            services.AddScoped<GetUserService>();
            services.AddScoped<GetHomeProjectByProjectId>();
            services.AddScoped<CreateHomeProjectService>();
            services.AddScoped<DeleteHomeProjectService>();
            services.AddScoped<GetAllHomeProjects>();
            services.AddScoped<GetHomeProjectsByClientNameService>();
            services.AddScoped<GetHomeProjectsByProjectNameService>();
            services.AddScoped<GetHomeProjectsByStatusService>();
            services.AddScoped<UpdateHomeProjectService>();
            services.AddScoped<HomeProjectsService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DTH_HomeProjectAPI");
                c.RoutePrefix = "swagger"; 
            });

            app.UseMiddleware<BasicAuthMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
