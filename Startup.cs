using System.Text.Json.Serialization;
using DTH.API.Data;
using DTH.API.Services;
using Microsoft.EntityFrameworkCore;

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
                });
            services.AddDbContext<HomeProjectDbContext>(options =>
                options.UseSqlite("Data Source=HomeProjects.db"));
            services.AddLogging();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
