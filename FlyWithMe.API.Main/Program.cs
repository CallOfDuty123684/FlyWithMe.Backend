namespace FlyWithMe.API.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services); 

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers(options =>
            {
                //options.Filters.Add<JwtValidationFilter>(); 
            });

            // Enable CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5000") 
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials(); 
                });
            });

            var app = builder.Build();

            app.UseSession(); 
            app.UseRouting();
            app.UseCors("AllowFrontend");
            app.UseAuthorization();

            startup.Configure(app, builder.Environment); 

            app.Run();
        }
    }
}
