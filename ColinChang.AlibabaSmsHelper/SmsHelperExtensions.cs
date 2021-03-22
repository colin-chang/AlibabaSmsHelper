using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ColinChang.AlibabaSmsHelper
{
    public static class SmsHelperExtensions
    {
        public static IServiceCollection AddSmsHelper(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<SmsHelperOptions>()
                .Configure(config.Bind)
                .ValidateDataAnnotations();
            services.AddSingleton<IOptionsChangeTokenSource<SmsHelperOptions>>(
                new ConfigurationChangeTokenSource<SmsHelperOptions>(config));
            
            services.AddSingleton<ISmsHelper, SmsHelper>();
            return services;
        }
    }
}