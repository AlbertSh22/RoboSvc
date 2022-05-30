using Microsoft.EntityFrameworkCore;

using EntityDal.Context;

namespace PublicApi.Configuration
{
    /// <summary>
    /// Extension methods for setting up Entity Framework Core related.
    /// </summary>
    public static class ConfigureDbContext
    {
        /// <summary>
        ///     Configures the context to connect to a RoboSvc database.
        /// </summary>
        /// <param name="services">
        ///     The IServiceCollection to add the DB context to.
        /// </param>
        /// <returns>
        ///     A reference to this instance after the operation has completed.
        /// </returns>
        public static IServiceCollection AddDbContext(
            this IServiceCollection services
            )
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            services.AddDbContext<RoboSvcContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("RoboSvcDatabase"))
            );

            return services;
        }
    }
}
