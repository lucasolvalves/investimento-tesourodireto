using Investimento.TesouroDireto.Domain.Interfaces.Services;
using Investimento.TesouroDireto.Domain.Services;
using Investimento.TesouroDireto.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Investimento.TesouroDireto.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITesouroDiretoService, TesouroDiretoService>();
            services.AddScoped<IB3TesouroDiretoService, B3TesouroDiretoService>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient("mockinvestimento", options =>
            {
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(PollyConfiguration.GetRetryPolicy());

            return services;
        }
    }
}
