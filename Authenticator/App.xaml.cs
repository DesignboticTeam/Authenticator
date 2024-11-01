using Authenticator.Components;
using Authenticator.UI_WPF;
using AuthenticatorConnector.Bootstrap;
using Connector.Bootstraper;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;

//using System.Runtime.InteropServices.JavaScript;
using System.Windows;
using UI_WPF.Factories;
using UI_WPF.Interfaces;
using UI_WPF.Services;
using UI_WPF.ViewModel;
using WPFLocalizeExtension.Engine;
using WPFLocalizeExtension.Providers;

namespace Authenticator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static IHost? AppHost { get; private set; }
        
        public App()
        {
            BootstrapLogger.InitializeLogger();

            string[] args = [];
            var builder = AuthenticatorBootstrap.CreateBuilder(null)
                .ConfigureServices(services =>
                {
                    BootstrapUI.ConfigureAuthentication(services);


                    // services.AddSingleton<IApplicationService, RevitApplication>();
                    //TODO move to UI boot
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainPage>();
                    services.AddSingleton<MainPageViewModel>();
                    services.AddSingleton<ViewModelFactory>();
                    var mapper = new ViewModelToViewMapper();
                    mapper.RegisterMapping<MainPageViewModel, MainPage>();
                });

            try {
                Log.Information("Building up");
                AppHost = builder.Build();
            }
            catch (Exception ex) {
                Log.Fatal(ex, "Application start-up failed");
                return;
            }
            finally {
              //  Log.CloseAndFlush();
            }



        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var res = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            var logger = AppHost.Services.GetRequiredService<ILogger<App>>();
            logger.LogInformation("Host started");
            logger.LogWarning("Host started");


            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            
            var mainPage = AppHost.Services.GetRequiredService<MainPage>();
            var mainPageViewModel = AppHost.Services.GetRequiredService<MainPageViewModel>();
            mainWindow.Content = mainPage;
            mainWindow.DataContext = mainPageViewModel;

         //   var initialNavigation = AppHost.Services.GetRequiredService<INavigationService>();
            var modalNavigation = AppHost.Services.GetRequiredService<NavigationModalService>();
            modalNavigation.NavigateTo<ModalLoginViewModel>();

            mainWindow.Show();
            /*
            initialNavigation.NavigateTo<InputViewModel>();

            //TODO make general runnig procedure based on config
            var projectService = AppHost.Services.GetRequiredService<IProjectService<AnalyzerProject>>();
            await projectService.CreateProject();

            //  mainWindow.DataContext = AppHost.Services.GetRequiredService<TestViewModel>();
            */
            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            var logger = AppHost.Services.GetRequiredService<ILogger<App>>();
            logger.LogInformation("Host exited");


            await AppHost.StopAsync();

            AppHost.Dispose();
            Log.CloseAndFlush(); // Ensure to flush and close the log
            base.OnExit(e);
        }
    }
}
