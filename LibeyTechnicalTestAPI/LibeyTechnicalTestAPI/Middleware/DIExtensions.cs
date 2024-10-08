using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application;
using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Infrastructure;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure;
using LibeyTechnicalTestDomain.Validations;
namespace LibeyTechnicalTestAPI.Middleware
{
    public static class DIExtensions
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services)
        {
            services.AddScoped<ILibeyUserAggregate, LibeyUserAggregate>();
            services.AddScoped<ILibeyUserRepository, LibeyUserRepository>();
            services.AddScoped<ILibeyUbigeoAggregate, LibeyUbigeoAggregate>();
            services.AddScoped<ILibeyUbigeoRepository, LibeyUbigeoRepository>();
            services.AddTransient<ValidatorService>();

            return services;
        }
    }
}
