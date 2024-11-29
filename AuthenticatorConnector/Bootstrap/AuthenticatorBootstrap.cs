using Connector;
using Connector.Configuration;
using Connector.DataModels;
using Connector.Exceptions;
using Connector.Interfaces;
using Connector.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
//using Serilog;
using System.IO;
using System.Reflection;

namespace AuthenticatorConnector.Bootstrap
{
    public static class AuthenticatorBootstrap
    {
        //TODO Move customConfig to Connector
        //TOOD Move enc to Connector with Utility and setup in Analyzer
        //TODO Add PostConfigFor AppSettings -> add paths
        //TODO add enc CustomSettings to project

        public static IHostBuilder CreateBuilderStandalone(string[]? args)
        {
            var executionFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            var specialConfigurator = new CustomConfigurationSource();
            
            var host = Host.CreateDefaultBuilder(args)
                //TODO move to Connector
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //Default stuff
                    var env = hostingContext.HostingEnvironment.EnvironmentName; //Set from DOTNET_ENVIRONMENT
                    config.SetBasePath(executionFolder)
                        //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        //.AddJsonFile("customsettings.json", optional: false, reloadOnChange: true)

                        //.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                        .AddCustomConfigurationJsonFile("appsettings.enc", "DesingboticTools")
                        .AddCustomConfigurationJsonFile("customsettings.enc", "DesingboticTools")

                        .AddEnvironmentVariables();
                        //.Build();

                  //  config.AddEncryptedJsonFile(Path.Combine(executionFolder, "appsettings.json"), "DesignboticTools").Build();
                })
                //.UseSerilog()
                .ConfigureServices((hostingContext, services) =>
                {

                    services.Configure<AppSettings>(hostingContext.Configuration.GetSection("AppSettings"));
                    services.Configure<CustomSettings>(hostingContext.Configuration.GetSection("CustomSettings"));

                    services.AddSingleton<IPostConfigureOptions<CustomSettings>, CustomSettingsPostConfigure>();
                    services.AddSingleton<IPostConfigureOptions<AppSettings>, AppSettingsPostConfigure>();

                    services.ConfigureServicesAuthenticator();
                    services.AddNeededServicesIfNotAdded();
                });

            return host;
        }
            
        /// <summary>
        /// Configurates UI for Authentication as Service in App. NOTE: Use last after all UI Services are added.
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="mapper">ViewModel Mapper -> add all other mappings before -> mapper will be added to ServiceCollector</param>
        /// <param name="ensureNeedServices">Ensure to initilize needed Services for UI to work</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServicesAuthenticator(this IServiceCollection services, bool ensureNeedServices = true)
        {
            //Add AuthModals

            services.AddSingleton<IAuthenticationService, FirebaseAuthenticationService>();
            services.TryAddSingleton<IPostConfigureOptions<CustomSettings>, CustomSettingsPostConfigure>();

            //Ensure needed services
            if (ensureNeedServices)
            {
                services.AddNeededServicesIfNotAdded();
            }

            return services;
        }

        /// <summary>
        /// Configure needed services if not initialized already -> should be used after all other initialization.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddNeededServicesIfNotAdded(this IServiceCollection services)
        {
            services.TryAddSingleton<IDialogService, DialogService>();
            services.TryAddSingleton<IMessagePrompter, MessagePrompter>();
            services.TryAddSingleton<IExceptionHandler, UserExceptionHandler>();
            services.TryAddSingleton<IDataService, FileService>();
            services.TryAddSingleton<IWebService, WebConnectorService>();
            return services;
        }
    } 
}
