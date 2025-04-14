using FlyWithMe.API.Main.Extensions;
using FlyWithMe.API.Persistence.Implementation;
using FlyWithMe.API.Persistence.Interfaces;
using FlyWithMe.API.Persistence.Models;
using FlyWithMe.API.Persistence.Services;
using Microsoft.EntityFrameworkCore;

namespace FlyWithMe.API.Main
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
            services.AddDbContext<FlyWithMeContext>(options => options.UseNpgsql(
               Configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly(typeof(FlyWithMeContext).Assembly.FullName))
           );

            #region Services
            services.AddScoped<IChatGPTService, ChatGPTService>();
            services.AddHttpClient<IAmadeusService, AmadeusService>();
            #endregion

            #region Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITourDetailsRepository, TourDetailsRepository>();
            services.AddScoped<IBlogDetailsRepository, BlogDetailsRepository>();
            #endregion

            services.AddSwaggerExtension();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddApiVersioningExtension();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSwaggerExtension();
            app.UseErrorHandlingMiddleware();
            app.UseDeveloperExceptionPage();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
