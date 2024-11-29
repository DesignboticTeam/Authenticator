using Authenticator.UI_WPF.Bootstrap;
using AuthenticatorConnector.Bootstrap;
using Connector.Configuration;
using Connector.DataModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using UI_WPF.Factories;
using UI_WPF.Interfaces;

namespace AuthenticatorWPF
{
    public static class Authenticator
    {

        public static void ConfigureAuthenticator(this IHostBuilder builder)
        {
            var executionFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {

                var env = hostingContext.HostingEnvironment.EnvironmentName;
                config.SetBasePath(executionFolder)
                    .AddCustomConfigurationJsonFile("customsettings.enc", "DesingboticTools");
            })
            .ConfigureServices((hostingContext, services) =>
            {
                services.Configure<CustomSettings>(hostingContext.Configuration.GetSection("CustomSettings"));

                services.ConfigureServicesAuthenticator();
                var test = services;
                /*
                var isMapperRegistered = services.Any(descriptor => descriptor.ServiceType == typeof(IViewModelToViewMapper));
                if (!isMapperRegistered) {
                    throw new Exception("Authenticator needs Mapper to Initialize");
                }
                */

                using (var serviceProvider = services.BuildServiceProvider()) {
                    var mapper = serviceProvider.GetService<IViewModelToViewMapper>();
                    if (mapper == null) {
                        throw new Exception("Authenticator needs Mapper to Initialize");
                    }
                    services.ConfigureServicesAuthenticatorUI(mapper, true);
                }

                //services.ConfigureServicesAuthenticatorUI(mapper, true);
            });
        }
    }
}


