using Microsoft.Extensions.DependencyInjection;

namespace FlyWithMe.API.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {

            //services.AddDbContext<SpeechTherapyContext>(options => options.UseNpgsql(
            //    connectionString,
            //    b => b.MigrationsAssembly(typeof(SpeechTherapyContext).Assembly.FullName))
            //);

        }
    }
}
