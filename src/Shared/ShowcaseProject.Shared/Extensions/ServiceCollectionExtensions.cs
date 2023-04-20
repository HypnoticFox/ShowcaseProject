using Microsoft.Extensions.DependencyInjection;

namespace ShowcaseProject.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services, string[] allowedOrigins, bool unsafeMode = false)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    if (unsafeMode)
                    {
                        policy
                        .SetIsOriginAllowed(origin => true) //Allows all origins
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("access-control-allow-origin", "access-control-allow-methods", "access-control-allow-headers", "access-control-allow-credentials", "access-control-max-age");
                    }
                    else
                    {
                        policy
                        .WithOrigins(allowedOrigins)
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
                    }
                });
        });

        return services;
    }
}
