using System;
using Microsoft.Extensions.DependencyInjection;

namespace Van.Winkel.Financial.Infrastructure.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigurationHelper(this IServiceCollection serviceCollection, Action<IConfigurationHelper> configuration)
        {
            serviceCollection.AddSingleton<IConfigurationHelper, ConfigurationHelper>(factory =>
            {
                var configurationHelper = new ConfigurationHelper();
                configuration(configurationHelper);
                return configurationHelper;
            });
        }
    }
}