using Connector;
using Connector.DataModels;
using Connector.Exceptions;
using Connector.Interfaces;
using Connector.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NextDesignerAnalysis.Daylight;
using NextDesignerAnalysis.Interfaces;
using NextDesignerElements;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AuthenticatorConnector.Bootstrap
{
    public static class AuthenticatorBootstrap
    {
        public static IHostBuilder CreateBuilder(string[]? args)
        {
            var executionFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var host = Host.CreateDefaultBuilder(args)
                //TODO move to Connector
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //Default stuff
                    var env = hostingContext.HostingEnvironment.EnvironmentName; //Set from DOTNET_ENVIRONMENT
                    config.SetBasePath(executionFolder)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables().Build();
                })
                .UseSerilog()
                .ConfigureServices((hostingContext, services) =>
                {
                    //Default stuff
                    services.Configure<AppSettings>(hostingContext.Configuration.GetSection("AppSettings"));

                    services.TryAddSingleton<IDialogService, DialogService>();
                    services.TryAddSingleton<IMessagePrompter, MessagePrompter>();
                    services.TryAddSingleton<IExceptionHandler, UserExceptionHandler>();

                    services.TryAddSingleton<IWebService, WebConnectorService>();
                    services.AddSingleton<IAuthenticationService, FirebaseAuthenticationService>();
                });

            return host;
        }
    } 
}
