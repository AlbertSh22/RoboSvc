using DomainLogic.Services;
using DomainLogic.Interfaces;

namespace PublicApi.Configuration
{
    /// <summary>
    ///     Extension methods for adding services to a IServiceCollection.
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        ///     Adds services for controllers to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">
        ///     The IServiceCollection to add the service to.
        /// </param>
        /// <returns>
        ///     A reference to this instance after the operation has completed.
        /// </returns>
        public static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            services.AddScoped<ILanguageService, LanguageService>();
            // etc ...

            return services;
        }
    }
}
